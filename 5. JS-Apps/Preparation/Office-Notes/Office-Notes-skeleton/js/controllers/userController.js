var app = app || {};

app.userController = (function (){
    function UserController(viewBag, model) {
        this._viewBag = viewBag;
        this._model = model;
    }

    function saveUserToStorage(data) {
        sessionStorage['sessionId'] = data._kmd.authtoken;
        sessionStorage['username'] = data.username;
        sessionStorage['fullName'] = data.fullName;
        sessionStorage['userId'] = data._id;
    }

    UserController.prototype.loadLoginPage = function(selector){
        this._viewBag.showLoginPage(selector);
    };

    UserController.prototype.login = function(data){
        return this._model.login(data)
            .then(function (success) {
                saveUserToStorage(success);

                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'You have successfully logged in!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/home/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while signing in!',
                    type: 'error',
                    timeout: 4000,
                    closeWith: ['click']
                });
            });
    };

    UserController.prototype.loadRegisterPage = function(selector){
        this._viewBag.showRegisterPage(selector);
    };

    UserController.prototype.register = function(data){
        return this._model.register(data)
            .then(function (success) {
                saveUserToStorage(success);

                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'You have successfully registered and logged in!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/home/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while registering user!',
                    type:'error',
                    timeout: 4000,
                    closeWith: ['click']
                });
            });
    };

    UserController.prototype.logout = function(data){
        return this._model.logout()
            .then(function (success) {
                sessionStorage.clear();

                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'You have successfully logged out!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while logging out!',
                    type:'error',
                    timeout: 4000,
                    closeWith: ['click']
                });
            });
    };

    return {
        load: function (viewBag, model) {
            return new UserController(viewBag, model);
        }
    };
}());