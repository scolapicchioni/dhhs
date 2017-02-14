class DataLayer {
    constructor() {
        this.url = "http://localhost:5000/swagger/v1/swagger.json";
    }
    getProducts() {
        return new SwaggerClient({
            url: this.url,
            usePromise: true
        }).then(client=> {
            return client.Products.ApiProductsGet(null, { responseContentType: 'application/json' });
        });
    }
        
    getProductById(id) {
        return new SwaggerClient({
            url: this.url,
            usePromise: true
        }).then(client=> {
            return client.Products.ApiProductsByIdGet({ id }, { responseContentType: 'application/json' });
        });
    }

    insertProduct(product) {
        return new SwaggerClient({
            url: this.url,
            usePromise: true
        }).then(client=> {
            return client.Products.ApiProductsPost({ product }, { responseContentType: 'application/json' });
        });
    }

    updateProduct(id, product) {
        return new SwaggerClient({
            url: this.url,
            usePromise: true
        }).then(client=> {
            return client.Products.ApiProductsByIdPut({ id, product }, { responseContentType: 'application/json' });
        });
    }

    deleteProduct(id) {
        return new SwaggerClient({
            url: this.url,
            usePromise: true
        }).then(client=> {
            return client.Products.ApiProductsByIdDelete({ id }, { responseContentType: 'application/json' });
        });
    }
}