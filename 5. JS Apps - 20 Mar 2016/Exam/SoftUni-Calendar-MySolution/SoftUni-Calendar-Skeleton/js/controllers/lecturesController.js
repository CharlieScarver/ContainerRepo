var app = app || {};

app.lecturesController = (function(){
     function LecturesController(viewBag, model) {
         this._viewBag = viewBag;
         this._model = model;
     }

    LecturesController.prototype.loadAllLectures = function(menuSelector, mainSelector) {
         var _this = this;
         return this._model.getAllLectures()
             .then(function (success) {
                 _this._viewBag.showAllLectures(menuSelector, mainSelector, success);
             })
             .done();
     };

    LecturesController.prototype.loadMyLectures = function(menuSelector, mainSelector) {
        var _this = this;
        return this._model.getMyLectures()
            .then(function (success) {
                _this._viewBag.showMyLectures(menuSelector, mainSelector, success);
            })
            .done();
    };

    LecturesController.prototype.loadAddLecture = function (menuSelector, mainSelector) {
        this._viewBag.showAddLecture(menuSelector, mainSelector);
    };

    LecturesController.prototype.addLecture = function (data) {
        var result = {
            title: data.title,
            start: data.start,
            end: data.end,
            lecturer: sessionStorage['username']
        };

        return this._model.addLecture(result)
            .then(function (success) {
                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'Lecture was successfully added!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/calendar/my/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while adding lecture!',
                    type:'error',
                    timeout: 2000,
                    closeWith: ['click']
                });
            });
    };

    LecturesController.prototype.loadEditLecture = function (menuSelector, mainSelector, lectureId) {
        var _this = this;
        return this._model.getLectureById(lectureId)
            .then(function (success) {
                _this._viewBag.showEditLecture(menuSelector, mainSelector, success);
            });
    };

    LecturesController.prototype.editLecture = function (data) {
        var result = {
            title: data.title,
            start: data.start,
            end: data.end,
            lecturer: sessionStorage['username'],
            _id: data._id
        };
        return this._model.editLecture(result._id, result)
            .then(function (success) {
                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'Lecture was successfully edited!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/calendar/my/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while editing lecture!',
                    type:'error',
                    timeout: 2000,
                    closeWith: ['click']
                });
            });
    };

    LecturesController.prototype.loadDeleteLecture = function (menuSelector, mainSelector, lectureId) {
        var _this = this;
        return this._model.getLectureById(lectureId)
            .then(function (success) {
                _this._viewBag.showDeleteLecture(menuSelector, mainSelector, success);
            });
    };

    LecturesController.prototype.deleteLecture = function (lectureId) {
        return this._model.deleteLecture(lectureId)
            .then(function (success) {
                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'Lecture was successfully deleted!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/calendar/my/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while deleting lecture!',
                    type:'error',
                    timeout: 2000,
                    closeWith: ['click']
                });
            });
    };

    return {
         load: function (viewBag, model) {
             return new LecturesController(viewBag, model);
         }
     };
 }());