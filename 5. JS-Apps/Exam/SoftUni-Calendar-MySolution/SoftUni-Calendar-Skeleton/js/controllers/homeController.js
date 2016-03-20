var app = app || {};

app.homeController = (function (){
    function HomeController(viewBag, model) {
        this._viewBag = viewBag;
        this._model = model;
    }


    HomeController.prototype.loadWelcomeGuest = function(menuSelector, mainSelector){
        this._viewBag.showWelcomeGuest(menuSelector, mainSelector);
    };

    HomeController.prototype.loadWelcomeUser = function(menuSelector, mainSelector){
        var data = {
            username: sessionStorage['username']
        };

        this._viewBag.showWelcomeUser(menuSelector, mainSelector, data);
    };

    return {
        load: function (viewBag, model) {
            return new HomeController(viewBag, model);
        }
    };
}());