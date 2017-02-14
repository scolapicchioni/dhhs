/// <reference path="oidc-client.js" />

class ViewModel {
    constructor() {
        const config = {
            authority: "http://localhost:5000",
            client_id: "js",
            redirect_uri: "http://localhost:5003/callback.html",
            response_type: "id_token token",
            scope: "openid profile api1",
            post_logout_redirect_uri: "http://localhost:5003/index.html"
        };

        this.mgr = new Oidc.UserManager(config);

        this.mgr.getUser().then(user => {
            if (user) 
                this.log("User logged in", user.profile);
            else 
                this.log("User not logged in");
        }).catch(error => this.log("Problem trying to read the user", error));

        window.onload = () => {
            document.getElementById("login").addEventListener("click", ()=> this.login(), false);
            document.getElementById("api").addEventListener("click", ()=> this.api(), false);
            document.getElementById("logout").addEventListener("click", () => this.logout(), false);
        };
    }


    log(...parameters) {
        document.getElementById('results').innerText = '';
        
        for(const parameter of parameters){
            let msg;
            if (parameter instanceof Error) 
                msg = `Error: ${parameter.message}`;
            else if (typeof parameter !== 'string') 
                msg = JSON.stringify(parameter, null, 2);
            else
                msg = parameter;
            document.getElementById('results').innerHTML += `${msg} \r\n`;
        }
    }

    login() {
        this.mgr
            .signinRedirect()
            .catch(error => this.log("problem during signinRedirect: " , error));
    }

    api() {
        this.mgr.getUser().then(user => {
            const url = "http://localhost:5001/identity";

            const xhr = new XMLHttpRequest();
            xhr.open("GET", url);
            xhr.onload =  () => this.log(xhr.status, JSON.parse(xhr.responseText));
            xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
            xhr.send();
        }).catch(problem => this.log("problem calling the api: ", problem));
    }

    logout() {
        this.mgr.signoutRedirect().catch(error => this.log("problem during signoutRedirect: ", error));
    }
}



new ViewModel();