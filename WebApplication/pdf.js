module.exports = function (callback, data) {
    var jsreport = require('jsreport-core')();

    jsreport.init().then(function () {
        return jsreport.render({
            template: {
                content: data,
                engine: 'jsrender',
                recipe: 'phantom-pdf',
                phantom: {
                    format: 'A4',
                    margin: { top: 0, right: 0, bottom: 0, left: 0 },
                    orientation: "landscape"
                }
            }
        }).then(function (resp) {
            callback(null, resp.content.toJSON().data);
        });
    }).catch(function (e) {
        callback(e, null);
    })
};