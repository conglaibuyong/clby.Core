var Lookup = function (c, p, options) {
    if (!c || !p || !options || !options.from || !options.localField || !options.foreignField || !options.as) {
        return { result: false, msg: "Lookup.Args", json: { c: c, p: p, options: options } };
    }

    var view = db.getCollection(c).aggregate(p).toArray();
    if (ret.length == 0) {
        return { result: true, msg: "", json: [] };
    }

    var tmp_Arr = [];
    view.forEach(function (v) {
        v[options.as] = [];
        tmp_Arr.push(v[options.localField]);
    });
    var query = {};
    query[options.foreignField] = { "$in": tmp_Arr };
    var tt = db.getCollection(options.from).find(query).toArray();
    if (tt.length > 0) {
        for (var i in tt) {
            for (var v in view) {
                if (tt[i][options.foreignField] == view[v][options.localField]) {
                    view[v][options.as].push(tt[i]);
                }
            }
        }
    }

    return { result: true, msg: "", json: view };
};