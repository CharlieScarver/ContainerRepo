var app = app || {};


(function (){
    var selector = '#container';
    var requester = app.requester.load(
        'kid_bknCD6GMyW',
        '1f852ff3dc794bfda66e49be73c54780',
        'https://baas.kinvey.com/'
    );

    var userModel = app.userModel.load(requester);
    var notesModel = app.notesModel.load(requester);

    var userViewBag = app.userViewBag.load();
    var homeViewBag = app.homeViewBag.load();
    var notesViewBag = app.notesViewBag.load();

    var userController = app.userController.load(userViewBag, userModel);
    var homeController = app.homeController.load(homeViewBag);
    var notesController = app.notesController.load(notesViewBag, notesModel);

    var router = Sammy(function () {

        this.before({except:'#\/(login\/|register\/)?'}, function () {
            if (!sessionStorage['sessionId']) {
                noty({
                    theme: 'relax',
                    text: 'You should be logged in to do this action!',
                    type:'error',
                    timeout: 4000,
                    closeWith: ['click']
                });
                this.redirect('#/');
                return false;
            }
        });

        this.before({only: '#\/(login\/|register\/)?'}, function () {
            if (sessionStorage['sessionId']) {
                this.redirect('#/home/');
                return false;
            }
        });

        this.before(function () {
            if (!sessionStorage['sessionId']) {
                $('#menu').hide();
            } else {
                $('#menu').show();
                $('#welcomeMenu').text('Welcome, ' + sessionStorage['username']);
            }
        });

        this.get('#/home/', function () {
            homeController.loadHomePage(selector);
        });

        this.get('#/', function () {
            homeController.loadWelcomePage(selector);
        });

        this.get('#/login/', function () {
           userController.loadLoginPage(selector);
        });

        this.get('#/register/', function () {
            userController.loadRegisterPage(selector);
        });

        this.get('#/logout/', function () {
           userController.logout();
        });

        this.get('#/office/', function () {
            notesController.loadOfficeNotes(selector);
        });

        this.get('#/myNotes/', function () {
            notesController.loadMyNotes(selector);
        });

        /*
        this.get('#/myNotes/:id', function () {
            notesController.loadEditNote(selector, this.params['id']);
        });
        */

        this.get('#/addNote/', function () {
           notesController.loadAddNote(selector);
        });

        this.get("", function () {
            // avoiding sammy 404 error
        });

        this.bind('redirectUrl', function (ev, data) {
            this.redirect(data.url);
        });

        this.bind('login', function (ev, data) {
            userController.login(data);
        });

        this.bind('register', function (ev, data) {
            userController.register(data);
        });

        this.bind('addNote', function (ev, data) {
            notesController.addNote(data);
        });

        this.bind('editNote', function (ev, data) {
            notesController.editNote(data.id, data);
        });

        this.bind('showEditNote', function (ev, data) {
            notesController.loadEditNote(selector, data);
        });

        this.bind('deleteNote', function (ev, data) {
            notesController.deleteNote(data.id);
        });

        this.bind('showDeleteNote', function (ev, data) {
            notesController.loadDeleteNote(selector, data);
        });

    });

    router.run('#/');
}());