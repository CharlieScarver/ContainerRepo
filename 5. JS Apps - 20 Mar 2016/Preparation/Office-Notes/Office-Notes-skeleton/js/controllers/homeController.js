var app = app || {};

app.homeController = (function (){
    function HomeController(viewBag, model) {
        this._viewBag = viewBag;
        this._model = model;
    }
    

    HomeController.prototype.loadWelcomePage = function(selector){
        this._viewBag.showWelcomePage(selector);
    };

    HomeController.prototype.loadHomePage = function(selector){
        var data = {
            fullName: sessionStorage['fullName'],
            username: sessionStorage['username']
        };

        this._viewBag.showHomePage(selector, data);
    };
    
    return {
        load: function (viewBag, model) {
            return new HomeController(viewBag, model);
        }
    };
}());