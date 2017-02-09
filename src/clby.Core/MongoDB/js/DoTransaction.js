﻿var DoTransaction = function (tid) {
    var t = db.Transactions.findOne({ _id: ObjectId(tid), Status: 0 });
    if (!t) {
        return { result: false, msg: "Transaction.NotFind:" + tid };
    }
    var tmp_Result = true;
    if (!t.Items || t.Items.length < 2) {
        tmp_Result = false;
    } else {
        for (var i in t.Items) {
            if (db.getCollection(t.Items[i].CollectionName).count(JSON.parse(t.Items[i].Query)) == 0) {
                tmp_Result = false;
            }
        }
    }
    if (!tmp_Result) {
        db.Transactions.update({ _id: ObjectId(tid) }, { $set: { Status: 4 }, $currentDate: { _LastModyTime: true } });
        return { result: false, msg: "Transaction.NoMatched:" + tid };
    }
    db.Transactions.update({ _id: ObjectId(tid) }, { $set: { Status: 1 }, $currentDate: { _LastModyTime: true } });
    var tmp_ret1 = [];
    for (var i in t.Items) {
        var query = JSON.parse(t.Items[i].Query);
        var update = JSON.parse(t.Items[i].Update);
        query["_Transactions"] = { $ne: ObjectId(tid) };
        if (!update["$push"]) {
            update["$push"] = { _Transactions: ObjectId(tid) };
        } else {
            update["$push"]["_Transactions"] = ObjectId(tid);
        }
        if (!update["$currentDate"] && !update["$currentDate"]["_LastModyTime"]) {
            update["$currentDate"] = { _LastModyTime: true };
        }
        if (!update["$set"]) {
            update["$set"] = { _Version: ObjectId().str };
        } else {
            update["$set"]["_Version"] = ObjectId().str;
        }
        var tmp_u_ret = db.getCollection(t.Items[i].CollectionName).update(query, update);
        tmp_ret1.push(JSON.stringify(tmp_u_ret));
    }
    db.Transactions.update({ _id: ObjectId(tid) }, { $set: { Status: 2 }, $push: { Results: tmp_ret1 }, $currentDate: { _LastModyTime: true } });
    var tmp_ret2 = [];
    for (var i in t.Items) {
        var query = JSON.parse(t.Items[i].Query);
        var update = {};
        query["_Transactions"] = ObjectId(tid);
        update["$pull"] = { _Transactions: ObjectId(tid) };
        update["$currentDate"] = { _LastModyTime: true };
        update["$set"] = { _Version: ObjectId().str };
        var tmp_u_ret = db.getCollection(t.Items[i].CollectionName).update(query, update);
        tmp_ret2.push(JSON.stringify(tmp_u_ret));
    }
    db.Transactions.update({ _id: ObjectId(tid) }, { $set: { Status: 3 }, $push: { Results: tmp_ret2 }, $currentDate: { _LastModyTime: true } });

    return { result: true, msg: "Transaction.Done:" + tid };
};