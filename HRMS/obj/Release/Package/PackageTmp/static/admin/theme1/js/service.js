// Jquery service class.
function Service() {

    ////this.AjaxGet = function (url, async, preAsyncEvent, postAsyncEvent, callback, callbackparameter, eventName, successCallback) {
    ////    var returnValue;
    ////    if (async == undefined) {
    ////        async = false;
    ////    }
    ////    $.ajax({
    ////        url: url,
    ////        type: 'GET',
    ////        contentType: 'application/json; charset=utf-8',
    ////        //headers: {
    ////        //    'Authorization-Token': thisObject.Base.ServiceUtilities.getItem(sessionTokenKey)
    ////        //},
    ////        cache: false,
    ////        async: async,
    ////        beforeSend: function () {
    ////            if (preAsyncEvent != undefined) {
    ////                preAsyncEvent();
    ////            }
    ////        },
    ////        complete: function () {
    ////            if (postAsyncEvent != undefined) {
    ////                postAsyncEvent();
    ////            }
    ////        },
    ////        success: successCallback,
    ////        error: function (req, status, error) {
    ////            // returnValue = [false, JSON.parse(req.responseText).Message];
    ////            returnValue = [false, error.message];
    ////            if (async == true && eventName != undefined) {
    ////                $(this).trigger(eventName, [returnValue]);
    ////            }
    ////            if (async == true && callback != undefined) {
    ////                callback(returnValue, callbackparameter);
    ////            }
    ////        }
    ////    });

    ////    return returnValue;       
    ////}

    this.AjaxGet = function (url, async, successCallback) {
        if (async == undefined) {
            async = false;
        }
        //debugger;
        $.ajax({
            type: 'GET',
            url:  url,
            contentType: 'application/json;',
            dataType: 'json',
            async: async,
            success: successCallback,
            error: function (xhr, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }
    this.get = function (url, async, preAsyncEvent, postAsyncEvent, callback, callbackparameter, eventName) {

        var returnValue;
        if (async == undefined) {
            async = false;
        }
        $.ajax({
            url: url,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            //headers: {
            //    'Authorization-Token': thisObject.Base.ServiceUtilities.getItem(sessionTokenKey)
            //},
            cache: false,
            async: async,
            beforeSend: function () {
                if (preAsyncEvent != undefined) {
                    preAsyncEvent();
                }
            },
            complete: function () {
                if (postAsyncEvent != undefined) {
                    postAsyncEvent();
                }
            },
            success: function (result) {
                returnValue = [true, result];
                if (async == true && eventName != undefined) {
                    $(this).trigger(eventName, [returnValue]);
                }
                if (async == true && callback != undefined) {
                    callback(returnValue, callbackparameter);
                }
            },
            error: function (req, status, error) {
                // returnValue = [false, JSON.parse(req.responseText).Message];
                returnValue = [false, error.message];
                if (async == true && eventName != undefined) {
                    $(this).trigger(eventName, [returnValue]);
                }
                if (async == true && callback != undefined) {
                    callback(returnValue, callbackparameter);
                }
            }
        });

        return returnValue;
    }
    /*this.post = function (url, object, async) {
        var returnValue;
        if (async == undefined) {
            async = false;
        }
        object = JSON.stringify(object);
        $.ajax({
            url: url,
            type: 'POST',
            data: object,
            contentType: 'application/json; charset=utf-8',          
            cache: false,
            async: async,            
            success: function (user, status, XHR) {               
                returnValue = [true, user, XHR];               
            },
            error: function (req, status, error) {
                try {                    
                        return [false, error.message];                    
                }
                catch (err)
                {
                    returnValue = [false];               
                }
            }
        });

        return returnValue;
    }*/
    this.post = function (url, object, async, successCallback) {     
        if (async == undefined) {
            async = false;
        }        
        object = JSON.stringify(object);
        $.ajax({
            url: url,
            type: 'POST',
            data: object,
            contentType: 'application/json; charset=utf-8',
            cache: false,
            async: async,
            success: successCallback,
            error: function (xhr, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });

       
    }

    this.put = function (url, recordasync, async, preAsyncEvent, postAsyncEvent, callback, callbackparameter, eventName, preEventParameters, progressLoaderContainerId) {
        var returnValue;
        if (async == undefined) {
            async = false;
        }

        $.ajax({
            url: url,
            type: 'PUT',
            data: recordasync,
            contentType: 'application/json; charset=utf-8',            
            cache: false,
            async: async,
            beforeSend: function () {
                preRequestProcessing(progressLoaderContainerId, preAsyncEvent, preEventParameters);
            },
            complete: function () {
                postRequestProcessing(progressLoaderContainerId, postAsyncEvent, preEventParameters);
            },
            success: function (user, status, XHR) {
                returnValue = [true, XHR];
                if (async == true && eventName != undefined) {
                    $(this).trigger(eventName, [returnValue]);
                }
                if (async == true && callback != undefined) {
                    callback(returnValue, callbackparameter);
                }
            },
            error: function (req, status, error) {
               // returnValue = [false, JSON.parse(req.responseText).Message];
                returnValue = [false, JSON.parse(req).Message];
                if (async == true && eventName != undefined) {
                    $(this).trigger(eventName, [returnValue]);
                }
                if (async == true && callback != undefined) {
                    callback(returnValue, callbackparameter);
                }
            }
        });

        return returnValue;
    }
    this.del = function (url, object) {
        var returnValue;

        $.ajax({
            url: url,
            type: 'DELETE',
            data: object,
            contentType: 'application/json; charset=utf-8',
            //headers: {
            //    'Authorization-Token': thisObject.Base.ServiceUtilities.getItem(sessionTokenKey)
            //},
            cache: false,
            async: false,
            success: function (user) {
                returnValue = [true, ""];
            },
            error: function (req, status, error) {
                //  returnValue = [false, JSON.parse(req.responseText).Message];
                returnValue = [false, error.Message];
            }

        });

        return returnValue;
    }

    this.makeGetRequest = function (url, successCallback, errCallback) {
        var req = new XMLHttpRequest();
        req.open('GET', url);
        req.onload = function () {
            if (req.status == 200) {
            
                //successCallback(JSON.parse(req.response));
                return successCallback(req.response);
            }
            else {
              
                return  errCallback({ code: req.status, message: req.statusText });
            }
        };
        // make the request
        req.send();
    }



    function preRequestProcessing(progressLoaderContainerId, preAsyncEvent, preEventParameters) {
        
        if (progressLoaderContainerId != null) {
            //__DOMUtils.setProgressLoader(progressLoaderContainerId);
            LoadingDetail(progressLoaderContainerId);
        }
        else if (preAsyncEvent != undefined)
        {
            LoadingDetail();
        }

        //if (progressLoaderContainerId != null && $('#' + progressLoaderContainerId).length > 0)
        //    __DOMUtils.setProgressLoader(progressLoaderContainerId);
        //else if (preAsyncEvent != undefined)
        //    preAsyncEvent(preEventParameters);
    }
    function postRequestProcessing(progressLoaderContainerId, postAsyncEvent, preEventParameters) {
        UnloadDetails(progressLoaderContainerId);
        //if (progressLoaderContainerId != null) {
        //    //__DOMUtils.setProgressLoader(progressLoaderContainerId);
        //    UnloadDetails(progressLoaderContainerId);
        //}
        //else if (postAsyncEvent != undefined) {
        //    UnloadDetails();
        //}
        //if (progressLoaderContainerId != null && $('#' + progressLoaderContainerId).length > 0)
        //    __DOMUtils.removeProgressLoader(progressLoaderContainerId);
        //else if (postAsyncEvent != undefined)
        //    postAsyncEvent(preEventParameters);
    }

 
}