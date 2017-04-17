(function (){
    var mainSelector = '#container',
        menuSelector = '#menu';
    var requester = app.requester.load(
        'kid_ZkZ0VYWpyZ',
        '954cc1c11bae46c9a4c4447a7de36495',
        'https://baas.kinvey.com/'
    );

    var userModel = app.userModel.load(requester);
    var lecturesModel = app.lecturesModel.load(requester);

    var homeViewBag = app.homeViewBag.load();
    var userViewBag = app.userViewBag.load();
    var lecturesViewBag = app.lecturesViewBag.load();

    var homeController = app.homeController.load(homeViewBag);
    var userController = app.userController.load(userViewBag, userModel);
    var lecturesController = app.lecturesController.load(lecturesViewBag, lecturesModel);

    var router = Sammy(function () {

        this.before({except:'#\/(login\/|register\/)?'}, function () {
            if (!sessionStorage['sessionId']) {
                noty({
                    theme: 'relax',
                    text: 'You should be logged in to do this action!',
                    type:'error',
                    timeout: 2000,
                    closeWith: ['click']
                });
                this.redirect('#/');
                return false;
            }
        });

        this.before({only: '#\/(login\/|register\/)+'}, function () {
            if (sessionStorage['sessionId']) {
                this.redirect('#/');
                return false;
            }
        });

        this.get('#/', function () {
            if (!sessionStorage['sessionId']) {
                homeController.loadWelcomeGuest(menuSelector, mainSelector);
            } else {
                homeController.loadWelcomeUser(menuSelector, mainSelector);
            }
        });

        this.get('#/login/', function () {
            userController.loadLogin(menuSelector, mainSelector);
        });

        this.get('#/register/', function () {
            userController.loadRegister(menuSelector, mainSelector);
        });

        this.get('#/logout/', function () {
            userController.logout();
        });

        this.get('#/calendar/list/', function () {
            lecturesController.loadAllLectures(menuSelector, mainSelector);
        });

        this.get('#/calendar/my/', function () {
            lecturesController.loadMyLectures(menuSelector, mainSelector);
        });

        this.get('#/calendar/add/', function () {
            lecturesController.loadAddLecture(menuSelector, mainSelector);
        });

        this.get('#/calendar/edit/:id', function () {
            lecturesController.loadEditLecture(menuSelector, mainSelector, this.params['id']);
        });

        this.get('#/calendar/delete/:id', function () {
            lecturesController.loadDeleteLecture(menuSelector, mainSelector, this.params['id']);
        });

        // Custom Events

        this.bind('redirectUrl', function (ev, data) {
            this.redirect(data.url);
        });

        this.bind('login', function (ev, data) {
            userController.login(data);
        });

        this.bind('register', function (ev, data) {
            userController.register(data);
        });

        this.bind('addLecture', function (ev, data) {
           lecturesController.addLecture(data);
        });

        this.bind('editLecture', function (ev, data) {
            lecturesController.editLecture(data);
        });

        this.bind('deleteLecture', function (ev, data) {
            lecturesController.deleteLecture(data._id);
        });
    });

    router.run('#/');
}());