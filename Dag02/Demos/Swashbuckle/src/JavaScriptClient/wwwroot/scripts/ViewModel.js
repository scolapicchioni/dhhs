class ViewModel {
    constructor() {
        this.dl = new DataLayer();
        

        window.onload = () => {
            document.getElementById("get-products").addEventListener("click", () => {
                this.getAllProducts();
            });
            document.getElementById("get-product-by-id").addEventListener("click", () => {
                this.getProductById();
            });
            document.getElementById("post-product").addEventListener("click", () => {
                this.insertProduct();
            });
            document.getElementById("put-product").addEventListener("click", () => {
                this.updateProduct();
            });
            document.getElementById("delete-product").addEventListener("click", () => {
                this.deleteProduct();
            });
        }
    }

    showData(data) {
        document.getElementById("mydata").innerHTML = JSON.stringify(data);
    }

    getAllProducts() {
        this.dl.getProducts().then(data =>this.showData(data.data));
    }

    getProductById() {
        const id = document.getElementById("product-id").value;
        this.dl.getProductById(id).then(data=>this.showData(data.data));
    }

    insertProduct() {
        const id = 0; 
        const brand = document.getElementById("product-brand").value;
        const name = document.getElementById("product-name").value;
        const price = document.getElementById("product-price").value;

        this.dl.insertProduct({ id, brand, name, price }).then(data =>this.showData(data.data));
    }

    updateProduct() {
        const id = document.getElementById("product-id").value;
        const brand = document.getElementById("product-brand").value;
        const name = document.getElementById("product-name").value;
        const price = document.getElementById("product-price").value;

        this.dl.updateProduct(id, { id, brand, name, price }).then(data =>this.showData(data.data));
    }

    deleteProduct() {
        const id = document.getElementById("product-id").value;
        this.dl.deleteProduct(id).then(data =>this.showData(data.data));
    }
}