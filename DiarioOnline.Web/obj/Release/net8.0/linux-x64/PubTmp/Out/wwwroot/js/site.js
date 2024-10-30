const basePath = `${window.location.protocol}//${window.location.host}/DiarioOnline`;
//const basePathAPI = `${window.location.protocol}//${window.location.host}/api/`;
const basePathAPI = `${window.location.protocol}//localhost:7033/api/`;
var global = (function () {
    function pb() { }

    pb.basePath = function () {
        return basePath;
    }

    pb.ui = (function () {
        function _u() { }

        _u.showLoading = function (parent, txt, id, callback) {


            if (!parent) parent = "html";
            if (!txt) txt = "Carregando...";
            if (!id) id = pb.util.uid("loadingbox");
            var el = $(parent);
            var html =
                '<div id="' + id + '" data-izimodal-fullscreen="false" data-izimodal-title="" data-izimodal-subtitle="" data-izimodal-icon="icon-home" aria-hidden="false" aria-labelledby="modal-default" role="dialog" class="iziModal loadingbox" data-izimodal-group="grupo1" data-izimodal-loop="true" style="z-index: 999; border-radius: 3px; border-bottom: 3px solid rgb(136, 160, 185); overflow: hidden; max-width: 300px; display: block; height: 50px;">' +
                '<div class="iziModal-wrap" style = "height: auto;">' +
                '<div class="iziModal-content" style="padding: 20px;">' +
                '<div><i class="fa fa-spin fa-spinner"></i>Carregando</div>' +
                '</div>' +
                '</div></div>'
            el.append(html);

            return id;
        };
        _u.removeLoading = function (id) {
            if (!id) {
                _u.removeLoadingSelector(".loadingbox");
                return;
            }
            var selectorId = "#" + id;
            _u.removeLoadingSelector(selectorId);
        };
        _u.removeLoadingSelector = function (selector) {
            if (!selector) return;
            $(selector).remove();
        };

        _u.alert = function (msg, cbClose) {
            var xClose = pb.util.uid("close");
            var html = "<div style='padding: 15px; font-size: 18px; overflow-x: auto;'>" + msg + "</div>";
            html += "<div style='padding: 5px;' class='pull-right'><button id='" + xClose + "' class='btn btn-outline-secondary'>Fechar</button></div>";
            var _m = _u.modal("Aten&ccedil;&atilde;o", html, 500, false, "#" + xClose, null, null, {
            }, {
                headerColor: "#337ab7", closeButton: false, draggable: true
            });
            $("#" + xClose).on('click',function () {
                if (cbClose) cbClose(_m);
                _m.close();
            });
            _m.open();
            return _m;
        };
        _u.warning = function (msg, cbClose) {
            var xClose = pb.util.uid("close");
            var html = "<div style='padding: 15px; font-size: 16px; overflow-x: auto;'>" + msg + "</div>";
            html += "<div style='padding: 5px;' class='pull-right'><button id='" + xClose + "' class='btn btn-outline-secondary'>Fechar</button></div>";
            var _m = _u.modal("Aten&ccedil;&atilde;o", html, 500, false, "#" + xClose, null, null, {
            }, {
                headerColor: "#f39c12", closeButton: true, draggable: true
            });
            $("#" + xClose).on('click',function () {
                if (cbClose) cbClose(_m);
                _m.close();
            });
            _m.open();
            return _m;
        };
        _u.error = function (msg, cbOk) {
            var xOk = pb.util.uid("ok");
            var html = "<div style='padding: 15px; font-size: 18px; overflow-x: auto;'>" + msg + "</div>";
            html += "<div style='padding: 5px;' class='pull-right'><button id='" + xOk + "' class='btn btn-flat btn-outline-secondary'>Ok</button></div>";
            var _m = _u.modal("Erro!", html, 500, false, "#" + xOk, null, cbOk, {
            }, {
                headerColor: "#BD5B5B", icon: "fa fa-warning", draggable: true
            });
            $("#" + xOk).on('click',function () {
                if (cbOk) cbOk(_m);
                _m.close();
            });
            _m.open();
            return _m;
        };
        _u.confirm = function (msg, cbOk, cbCancel, width) {
            var xOk = pb.util.uid("ok");
            var xCancel = pb.util.uid("cancel");
            var html = "<div style='padding: 15px; font-size: 18px; overflow-x: auto;'>" + msg + "</div>";
            html += "<div style='padding: 5px;' class='pull-right'><button id='" + xOk + "' class='btn btn-flat btn-primary'>Ok</button> <button id='" + xCancel + "' class='btn btn-flat btn-outline-secondary'>Cancelar</button></div>";
            var _m = _u.modal("Confirma&ccedil;&atilde;o", html, (width ? width : 500), false, "#" + xCancel, null, null, {
            }, {
                icon: "fa fa-question-circle", draggable: true
            });
            $("#" + xOk).on('click',function () {
                if (cbOk) cbOk(_m);
                _m.close();
            });
            $("#" + xCancel).on('click',function () {
                if (cbCancel) cbCancel(_m);
                _m.close();
            });
            _m.open();
            return _m;
        };
        _u.question = function (msg, cbYes, cbNo, cbCancel) {
            var xYes = pb.util.uid("yes");
            var xNo = pb.util.uid("no");
            var xCancel = pb.util.uid("cancel");
            var html = "<div style='padding: 15px; font-size: 18px; overflow-x: auto;'>" + msg + "</div>";
            html += "<div style='padding: 5px;' class='pull-right'><button id='" + xYes + "' class='btn btn-flat btn-success'>Sim</button><button id='" + xNo + "' class='btn btn-flat btn-danger'>N&atilde;o</button> <button id='" + xCancel + "' class='btn btn-flat btn-outline-secondary'>Cancelar</button></div>";
            var _m = _u.modal(" ", html, 500, false, "#" + xCancel, null, null, {
            }, {
                icon: "fa fa-question-circle", draggable: true
            });
            $("#" + xYes).on('click',function () {
                if (cbYes) cbYes(_m);
                _m.close();
            });
            $("#" + xNo).on('click',function () {
                if (cbNo) cbNo(_m);
                _m.close();
            });
            $("#" + xCancel).on('click',function () {
                if (cbCancel) cbCancel(_m);
                _m.close();
            });
            _m.open();
            return _m;
        };

        _u.modal = (function () {
            var zidx = 9500; //Z-index inicial da modal
            function _m(title, html, width, fullscreen, selectorFocusOnOpen, onOpened, onClosed, cb, params) {
                if (!html) html = "";
                if (!cb) cb = {};
                if (!params) params = {};
                if (!cb.onOpening) cb.onOpening = function () {
                    //fix iziModal-isAttached
                    _m.qtd += 1;
                };
                if (!cb.onClosing) cb.onClosing = function () {
                    //fix iziModal-isAttached
                    _m.qtd -= 1;
                    if (_m.qtd < 0) _m.qtd = 0;
                    if (_m.qtd === 0) {
                        $("html").removeClass("iziModal-isAttached");
                    }
                };
                var _result = null;
                cb.onOpened = function (mod) {

                    //Cria evento de close para botão cancelar - utilizar id modalClose no botão
                    $("#" + mod.id + " #modalClose").on('click',function () {
                        mod.close();
                    });
                    //Coloca o focus no campo informado
                    if ("#" + mod.id + " " + selectorFocusOnOpen)
                        $("#" + mod.id + " " + selectorFocusOnOpen).focus();

                    if (onOpened)
                        onOpened(mod);
                };
                cb.onClosed = function (mod) {
                    if (onClosed)
                        onClosed(mod);
                };

                cb.onClosing;

                params.width = width;
                params.fullscreen = fullscreen;
                _result = _m.create(title, cb, params);
                _result.html(html);
                if (cb.onLoad) {
                    cb.onLoad(_result);
                }
                return _result;
            }
            _m.qtd = 0;
            _m.ajax = function (url, onLoad, width, fullscreen, selectorFocusOnOpen, onOpened, onClosed, cb, params) {
                if (!params) params = {};
                params.isAjax = true;
                if (!params.ajaxType) params.ajaxType = "GET";
                if (!params.postData) params.postData = null;
                global.util.ajax({
                    showLoading: true,
                    type: params.ajaxType,
                    dataType: "json",
                    contentType: params.contentType,
                    data: params.postData,
                    url: url
                }, function (result) {
                    params.inputParameters = {
                        url: url,
                        width: width,
                        onLoad: onLoad,
                        fullscreen: fullscreen,
                        selectorFocusOnOpen: selectorFocusOnOpen,
                        onOpened: onOpened,
                        onClosed: onClosed,
                        cb: cb,
                        params: params
                    };
                    var _mod = ui.modal(result.data.Titulo, result.data.Html, width, fullscreen, selectorFocusOnOpen, onOpened, onClosed, cb, params);
                    if (onLoad)
                        onLoad(_mod);
                    return _mod;
                }, function (result) {
                    return ui.error(result.jqXHR.responseText);
                }, function (result) {
                });
            };

            _m.create = function (title, cb, params) {
                var loading = global.ui.showLoading(null, "Carregando...", null, null);
                var xid = pb.util.uid("modal");
                var overlayClose = !pb.util.isNull(params.overlayClose) ? params.overlayClose : true;
                if (!params.removeOverlay) {
                    var xOverlay = "overlay_" + xid;
                    $("body").append("<div class='iziModal-overlay' style='display: none; background-color: rgba(0, 0, 0, 0.3);' id='" + xOverlay + "'></div>");
                }

                $("body").append("<div id='" + xid + "'></div>");
                if (!params) params = {
                };
                if (!cb) cb = {
                };
                if (!title) title = "";
                else title = "<span title='" + title + "'>" + title + "</span>";
                var _zidx = getZindex();

                var _modal = $("#" + xid).iziModal({
                    title: params.title ? params.title : title,
                    subtitle: params.subtitle ? params.subtitle : '',
                    headerColor: params.headerColor ? params.headerColor : '#3c8dbc',
                    background: params.background ? params.background : null,
                    theme: params.theme ? params.theme : '',  // light
                    icon: params.icon ? params.icon : null,
                    iconText: params.iconText ? params.iconText : null,
                    iconColor: params.iconColor ? params.iconColor : '',
                    rtl: params.rtl ? params.rtl : false,
                    width: params.width ? params.width : 600,
                    top: params.top ? params.top : null,
                    bottom: !pb.util.isNull(params.bottom) ? params.bottom : null,
                    borderBottom: params.borderBottom ? params.borderBottom : true,
                    padding: params.padding ? params.padding : 0,
                    radius: params.radius ? params.radius : 3,
                    zindex: params.zindex ? params.zindex : _zidx,
                    iframe: params.iframe ? params.iframe : false,
                    iframeHeight: params.iframeHeight ? params.iframeHeight : 400,
                    iframeURL: params.iframeURL ? params.iframeURL : null,
                    focusInput: params.focusInput ? params.focusInput : true,
                    group: params.group ? params.group : '',
                    loop: params.loop ? params.loop : false,
                    arrowKeys: params.arrowKeys ? params.arrowKeys : false,
                    navigateCaption: params.navigateCaption ? params.navigateCaption : true,
                    navigateArrows: params.navigateArrows ? params.navigateArrows : true, // Boolean, 'closeToModal', 'closeScreenEdge'
                    history: params.history ? params.history : false,
                    restoreDefaultContent: params.restoreDefaultContent ? params.restoreDefaultContent : false,
                    autoOpen: params.autoOpen ? params.autoOpen : 0, // Boolean, Number
                    bodyOverflow: params.bodyOverflow ? params.bodyOverflow : false,
                    fullscreen: params.fullscreen && !params.draggable ? params.fullscreen : false,
                    openFullscreen: params.openFullscreen ? params.openFullscreen : false,
                    closeOnEscape: params.closeOnEscape ? params.closeOnEscape : false,
                    closeButton: !pb.util.isNull(params.closeButton) ? params.closeButton : true,
                    appendTo: params.appendTo ? params.appendTo : 'body', // or false
                    appendToOverlay: params.appendToOverlay ? params.appendToOverlay : 'body', // or false
                    overlay: params.overlay ? params.overlay : false,
                    overlayClose: overlayClose,
                    overlayColor: params.overlayColor ? params.overlayColor : 'rgba(0, 0, 0, 0.4)',
                    timeout: params.timeout ? params.timeout : false,
                    timeoutProgressbar: params.timeoutProgressbar ? params.timeoutProgressbar : true,
                    pauseOnHover: params.pauseOnHover ? params.pauseOnHover : false,
                    timeoutProgressbarColor: params.timeoutProgressbarColor ? params.timeoutProgressbarColor : 'rgba(255,255,255,0.5)',
                    transitionIn: params.transitionIn ? params.transitionIn : false, //'fadeInUp',
                    transitionOut: params.transitionOut ? params.transitionOut : false, //'fadeOutUp',
                    transitionInOverlay: params.transitionInOverlay ? params.transitionInOverlay : false,
                    transitionOutOverlay: params.transitionOutOverlay ? params.transitionOutOverlay : false,
                    onFullscreen: cb.onFullscreen ? cb.onFullscreen : function () {
                    },
                    onResize: cb.onResize ? cb.onResize : function () {
                    },
                    onOpening: function () {
                        global.ui.removeLoading(loading);
                        if (params.draggable) {
                            $("#" + xid).draggable({
                                handle: ".iziModal-header"
                            });
                        }
                    },
                    onOpened: cb.onOpened ? cb.onOpened : function () {
                    },
                    onClosing: cb.onClosing && !cb.onClosed ? function () { dispose(); cb.onClosing(); } : function () {
                        dispose();
                        cb.onClosing();
                        cb.onClosed();
                    },
                    onClosed: cb.onClosed ? cb.onClosed : function () {
                    },
                    afterRender: cb.afterRender ? cb.afterRender : function () { }
                });

                function reload(oldModal) {
                    var i = params.inputParameters;
                    var newModal = null;
                    if (i && !pb.util.isNull(i.url)) {
                        newModal = _u.modal.ajax(i.url, i.onLoad, i.width, i.fullscreen, i.selectorFocusOnOpen, i.onOpened, i.onClosed, i.cb, i.params);
                        oldModal.close();
                    }
                    return newModal;
                }

                function getZindex() {
                    if (zidx >= 9990) zidx = 9500;
                    zidx = zidx + 5;
                    return zidx;
                }

                function dispose() {
                    $("#" + xid).remove();
                    $("#" + xOverlay).remove();
                }

                _modal.xid = xid;
                _modal.fn = $(_modal).iziModal;
                var methods = {
                    open: "open",
                    close: "close",
                    toggle: "toggle",
                    getState: "getState",
                    getGroup: "getGroup",
                    next: "next",
                    prev: "prev",
                    startLoading: "startLoading",
                    stopLoading: "stopLoading",
                    startProgress: "startProgress",
                    pauseProgress: "pauseProgress",
                    resumeProgress: "resumeProgress",
                    resetProgress: "resetProgress",
                    destroy: "destroy"
                };
                _modal.methods = methods;
                _modal.sel = function (selector) {
                    return "#" + xid + " " + selector;
                };
                _modal.el = function (selector) {
                    var el = null;
                    if (selector)
                        el = $(_modal.sel(selector));
                    else
                        el = $("#" + xid);
                    return el;
                };
                _modal.open = function () {
                    _modal.fn(methods.open); $("#" + xOverlay).css("z-index", _zidx - 3); $("#" + xOverlay).show(0);
                };
                _modal.close = function () {
                    _modal.fn(methods.close);
                    _modal.fn(methods.destroy);
                };
                _modal.toggle = function () {
                    _modal.fn(methods.toggle);
                };
                _modal.getState = function () {
                    _modal.fn(methods.getState);
                };
                _modal.getGroup = function () {
                    _modal.fn(methods.getGroup);
                };
                _modal.next = function () {
                    _modal.fn(methods.next);
                };
                _modal.prev = function () {
                    _modal.fn(methods.prev);
                };
                _modal.startLoading = function () {
                    _modal.fn(methods.startLoading);
                };
                _modal.stopLoading = function () {
                    _modal.fn(methods.stopLoading);
                };
                _modal.startProgress = function () {
                    _modal.fn(methods.startProgress);
                };
                _modal.pauseProgress = function () {
                    _modal.fn(methods.pauseProgress);
                };
                _modal.resumeProgress = function () {
                    _modal.fn(methods.resumeProgress);
                };
                _modal.resetProgress = function () {
                    _modal.fn(methods.resetProgress);
                };
                _modal.destroy = function () {
                    _modal.fn(methods.destroy);
                };
                _modal.html = function (_html) {
                    _modal.el(".iziModal-content").html(_html);
                };
                _modal.click = function (selector, callback) {
                    _modal.el(".iziModal-content").click(function (e) {
                        if (callback) callback(e);
                    });
                };
                if (params.isAjax)
                    _modal.reload = function () {
                        reload(_modal);
                    };
                if (cb.load) cb.load(xid, _modal);
                if (overlayClose) {
                    $("#" + xOverlay).on('click',function () {
                        _modal.close();
                    });
                }
                return _modal;
            };
            return _m;
        }());
        _u.notifications = {
            success: function (msg, timeout) {
                if (isNull(timeout)) timeout = 10000;
                if (isNull(msg)) msg = " ";
                var _m = global.ui.modal(msg, " ", 800, false, null, null, null, null, {
                    icon: 'icon-check',
                    headerColor: '#00af66',
                    timeout: timeout,
                    timeoutProgressbar: true,
                    transitionIn: 'fadeInUp',
                    transitionOut: 'fadeOutDown',
                    bottom: 0,
                    loop: true,
                    pauseOnHover: true,
                    removeOverlay: true
                });
                _m.open();
                return _m;
            },
            error: function (msg, timeout) {
                if (isNull(timeout)) timeout = 10000;
                if (isNull(msg)) msg = " ";
                var _m = global.ui.modal(msg, " ", 800, false, null, null, null, null, {
                    icon: 'icon-check',
                    headerColor: '#BD5B5B',
                    timeout: timeout,
                    timeoutProgressbar: true,
                    transitionIn: 'fadeInUp',
                    transitionOut: 'fadeOutDown',
                    bottom: 0,
                    loop: true,
                    pauseOnHover: true,
                    removeOverlay: true
                });
                _m.open();
                return _m;
            },
            warning: function (msg, timeout) {
                if (isNull(timeout)) timeout = 10000;
                if (isNull(msg)) msg = " ";
                var _m = global.ui.modal(msg, " ", 800, false, null, null, null, null, {
                    icon: 'icon-check',
                    headerColor: '#f39c12',
                    timeout: timeout,
                    timeoutProgressbar: true,
                    transitionIn: 'fadeInUp',
                    transitionOut: 'fadeOutDown',
                    bottom: 0,
                    loop: true,
                    pauseOnHover: true,
                    removeOverlay: true
                });
                _m.open();
                return _m;
            },
            info: function (msg, timeout) {
                if (isNull(timeout)) timeout = 10000;
                if (isNull(msg)) msg = " ";
                var _m = global.ui.modal(msg, " ", 800, false, null, null, null, null, {
                    icon: 'icon-check',
                    headerColor: '#00c0ef',
                    timeout: timeout,
                    timeoutProgressbar: true,
                    transitionIn: 'fadeInUp',
                    transitionOut: 'fadeOutDown',
                    bottom: 0,
                    loop: true,
                    pauseOnHover: true,
                    removeOverlay: true
                });
                _m.open();
                return _m;
            }
        };

        return _u;
    })();

    pb.util = (function () {

        function _u() { }

        _u.isNull = function (obj) {
            return typeof obj === "undefined" || obj === null;
        };
        _u.isNullOrEmpty = function (obj) {
            return _u.isNull(obj) || obj === "";
        };
        _u.isNullOrWhiteSpace = function (obj) {
            return _u.isNull(obj) || obj.trim() === "";
        };

        _u.uid = function (pre, callback) {
            if (!pre) pre = "x";
            var x = pre + "_" + Date.now() + "_" + Math.floor(Math.random() * 100) + 1;
            if (callback) callback(x);
            return x;
        };
        _u.uid = function (pre, callback) {
            if (!pre) pre = "x";
            var x = pre + "_" + Date.now() + "_" + Math.floor(Math.random() * 100) + 1;
            if (callback) callback(x);
            return x;
        };

        _u.pad = function (num, size) {
            var s = num + "";
            while (s.length < size) s = "0" + s;
            return s;
        };
        _u.toDateTime = function (datetime) {
            if (!datetime) return "";
            if (datetime.toString().indexOf("\/") > -1) {
                datetime = global.util.replaceAll(datetime, "\\/", "");
                datetime = eval("new " + datetime);
            }
            var d = new Date(datetime);
            var datestring = _u.pad(d.getDate(), 2) + "/" + _u.pad(d.getMonth() + 1, 2) + "/" + d.getFullYear() + " " +
                _u.pad(d.getHours(), 2) + ":" + _u.pad(d.getMinutes(), 2) + ":" + _u.pad(d.getSeconds(), 2);
            return datestring;
        };
        _u.getTime = function (datetime) {
            if (!datetime) return "";
            if (datetime.toString().indexOf("\/") > -1) {
                datetime = global.util.replaceAll(datetime, "\\/", "");
                datetime = eval("new " + datetime);
            }
            var d = new Date(datetime);
            var datestring = _u.pad(d.getHours(), 2) + ":" + _u.pad(d.getMinutes(), 2) + ":" + _u.pad(d.getSeconds(), 2);
            return datestring;
        };
        _u.getTimeFromTicks = function (ticks) {
            ticks = ticks / 1000;
            var hh = parseInt(Math.floor(ticks / 3600));
            var mm = parseInt(Math.floor((ticks % 3600) / 60));
            var ss = parseInt(ticks % 60);
            return (global.util.pad(hh, 2) + ":" + global.util.pad(mm, 2) + ":" + global.util.pad(ss, 2));
        };
        _u.toShortDateTime = function (datetime) {
            if (!datetime) return "";
            if (datetime.toString().indexOf("\/") > -1) {
                datetime = global.util.replaceAll(datetime, "\\/", "");
                datetime = eval("new " + datetime);
            }

            var d = new Date(datetime);
            var datestring = _u.pad(d.getDate(), 2) + "/" + _u.pad(d.getMonth() + 1, 2) + "/" + d.getFullYear();
            return datestring;
        };
        _u.toDateUSA = function (dateBr) {
            if (!dateBr) return "";
            if (dateBr.indexOf("\/") > -1) {
                dateBr = global.util.replaceAll(dateBr, "\\/", "");
            }
            var d = dateBr.substring(0, 2);
            var m = dateBr.substring(2, 4);
            var y = dateBr.substring(4, 8);
            return Date(m + "/" + d + "/" + y);
        };
        _u.ajax = function (params, callbackDone, callbackFail, callbackAways) {
            //console.log(JSON.stringify(params));
            var showLoading = params && params.showLoading;
            var showLoadingText = "Buscando dados...";
            if (params.showLoading && params.showLoading.text) {
                showLoadingText = params.showLoading.text;
            }
            var idLoading = showLoading ? pb.ui.showLoading(null, showLoadingText) : null;

            params.done = function (data, textStatus, jqXHR) {
                if (callbackDone) callbackDone({
                    data: data, textStatus: textStatus, jqXHR: jqXHR
                });
            };
            params.fail = function (jqXHR, textStatus, errorThrown) {
                if (callbackFail) callbackFail({
                    jqXHR: jqXHR, textStatus: textStatus, errorThrown: errorThrown
                });
            };
            params.aways = function (jqXHR, textStatus) {
                if (showLoading) pb.ui.removeLoading();
                if (callbackAways) callbackAways({
                    jqXHR: jqXHR, textStatus: textStatus
                });
            };
            params.success = params.done;
            params.error = params.fail;
            params.complete = params.aways;

            $.ajax(params);
        };

        _u.guidIsNullOrEmpty = function (guid) {
            return _u.isNullOrEmpty(guid) || guid === "00000000-0000-0000-0000-000000000000";
        };

        return _u;
    })();

    pb.event = (function () {
        function _e() {
        }

        _e.onKeyPress = function (selector, keyCode, callback) {
            $(selector).on('keypress',function (e) {
                if (e.keyCode === keyCode) {
                    if (callback)
                        callback(e);
                }
            });
        };

        _e.onPressEnter = function (selector, onPressEnter) {
            _e.onKeyPress(selector, 13, onPressEnter);
        };

        _e.onPressCtrlF5 = function (onPressCtrlF5) {
            $(window).off("keydown");
            $(window).on('keydown',function (e) {
                if (e.keyCode == 116 && e.ctrlKey) {

                    e.preventDefault();

                    global.security.getPermission(true, function (obj) {
                        console.log("Atualizando permissões.");
                        console.log(obj);

                        if (onPressCtrlF5)
                            onPressCtrlF5(e);

                        location.reload(true);
                    });
                }
            });
        };

        return _e;
    }());

    return pb;
})();


//Atalhos
var gb = global;
var ui = global.ui;
var util = global.util;
