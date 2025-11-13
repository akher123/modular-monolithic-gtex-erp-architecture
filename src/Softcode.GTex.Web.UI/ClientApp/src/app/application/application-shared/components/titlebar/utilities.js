"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * This enum is used to identify which type of toolbar will be populated for a page
 **/
var ToolbarType;
(function (ToolbarType) {
    ToolbarType[ToolbarType["ListPage"] = 1] = "ListPage";
    ToolbarType[ToolbarType["ListTabPage"] = 2] = "ListTabPage";
    ToolbarType[ToolbarType["DetailPage"] = 3] = "DetailPage";
    ToolbarType[ToolbarType["DetailTabPage"] = 4] = "DetailTabPage";
    ToolbarType[ToolbarType["LinkPage"] = 5] = "LinkPage";
    ToolbarType[ToolbarType["TabPage"] = 6] = "TabPage";
})(ToolbarType = exports.ToolbarType || (exports.ToolbarType = {}));
var DetailPageAction;
(function (DetailPageAction) {
    DetailPageAction[DetailPageAction["Save"] = 1] = "Save";
    DetailPageAction[DetailPageAction["SaveAndNew"] = 2] = "SaveAndNew";
    DetailPageAction[DetailPageAction["SaveAndClose"] = 3] = "SaveAndClose";
    DetailPageAction[DetailPageAction["Close"] = 4] = "Close";
})(DetailPageAction = exports.DetailPageAction || (exports.DetailPageAction = {}));
/**
 * Write all the possible pattern for the system we need [regular expression]
 * */
var PatterMatch = /** @class */ (function () {
    function PatterMatch() {
    }
    PatterMatch.EmailPattern = /\w+([-+.']\w+)*.?@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    return PatterMatch;
}());
exports.PatterMatch = PatterMatch;
//# sourceMappingURL=utilities.js.map