var app = app || {};

app.userController = (function (){
    function UserController(viewBag, model) {
        this._viewBag = viewBag;
        this._model = model;
    }

    function saveUserToStorage(data) {
        sessionStorage['sessionId'] = data._kmd.authtoken;
        sessionStorage['username'] = data.username;
        sessionStorage['userId'] = data._id;
    }

    UserController.prototype.loadLogin = function(menuSelector, mainSelector) {
        this._viewBag.showLoginPage(menuSelector, mainSelector);
    };

    UserController.prototype.login = function(data){
        return this._model.login(data)
            .then(function (success) {
                saveUserToStorage(success);

                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'Login successful!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while logging in!',
                    type: 'error',
                    timeout: 2000,
                    closeWith: ['click']
                });
            });
    };

    UserController.prototype.loadRegister = function(menuSelector, mainSelector) {
        this._viewBag.showRegisterPage(menuSelector, mainSelector);
    };

    UserController.prototype.register = function(data){
        return this._model.register(data)
            .then(function (success) {
                saveUserToStorage(success);

                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'Registration successful!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while registering!',
                    type:'error',
                    timeout: 2000,
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
                        text: 'Logout successful!',
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
                    timeout: 2000,
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