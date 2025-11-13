"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var environment_1 = require("../../../environments/environment");
var ApiHttpService = /** @class */ (function () {
    function ApiHttpService(http) {
        this.http = http;
        this.apiUrl = '';
        this.rootUrl = '';
        this.apiUrl = environment_1.environment.apiUrl;
        this.rootUrl = environment_1.environment.rootUrl;
    }
    ApiHttpService.prototype.GETDXGrid = function (url, loadOptions) {
        var params = new http_1.HttpParams()
            .set("requireTotalCount", loadOptions.requireTotalCount ? loadOptions.requireTotalCount : "")
            .set("primaryKey", loadOptions.primaryKey ? loadOptions.primaryKey : "")
            .set("remoteGrouping", loadOptions.remoteGrouping ? loadOptions.remoteGrouping : "")
            .set("remoteSelect", loadOptions.remoteSelect ? loadOptions.remoteSelect : "")
            .set("preSelect", loadOptions.preSelect ? loadOptions.preSelect : "")
            .set("select", loadOptions.select ? loadOptions.select : "")
            .set("groupSummary", loadOptions.groupSummary ? JSON.stringify(loadOptions.groupSummary) : "")
            .set("defaultSort", loadOptions.defaultSort ? JSON.stringify(loadOptions.defaultSort) : "")
            .set("totalSummary", loadOptions.totalSummary ? JSON.stringify(loadOptions.totalSummary) : "")
            .set("group", loadOptions.group ? JSON.stringify(loadOptions.group) : "")
            .set("sort", loadOptions.sort ? JSON.stringify(loadOptions.sort) : "")
            .set("take", loadOptions.take)
            .set("skip", loadOptions.skip)
            .set("isCountQuery", loadOptions.isCountQuery ? JSON.stringify(loadOptions.isCountQuery) : "")
            .set("requireGroupCount", loadOptions.requireGroupCount ? JSON.stringify(loadOptions.requireGroupCount) : "")
            .set("filter", loadOptions.filter ? JSON.stringify(loadOptions.filter) : "");
        //return this.http.get(this.apiUrl + url, { params: params }).toPromise().catch();
        return this.http.get(this.apiUrl + url, { params: params, responseType: 'json' });
    };
    ApiHttpService.prototype.GETGridData = function (url, loadOptions) {
        var params = new http_1.HttpParams()
            .set("requireTotalCount", loadOptions.requireTotalCount ? loadOptions.requireTotalCount : "")
            .set("primaryKey", loadOptions.primaryKey ? "" : "")
            .set("remoteGrouping", loadOptions.remoteGrouping ? loadOptions.remoteGrouping : "")
            .set("remoteSelect", loadOptions.remoteSelect ? loadOptions.remoteSelect : "")
            .set("preSelect", loadOptions.preSelect ? loadOptions.preSelect : "")
            .set("select", loadOptions.select ? loadOptions.select : "")
            .set("groupSummary", loadOptions.groupSummary ? JSON.stringify(loadOptions.groupSummary) : "")
            .set("defaultSort", loadOptions.defaultSort ? JSON.stringify(loadOptions.defaultSort) : "")
            .set("totalSummary", loadOptions.totalSummary ? JSON.stringify(loadOptions.totalSummary) : "")
            .set("group", loadOptions.group ? JSON.stringify(loadOptions.group) : "")
            .set("sort", loadOptions.sort ? JSON.stringify(loadOptions.sort) : "")
            .set("take", loadOptions.take)
            .set("skip", loadOptions.skip)
            .set("isCountQuery", loadOptions.isCountQuery ? JSON.stringify(loadOptions.isCountQuery) : "")
            .set("requireGroupCount", loadOptions.requireGroupCount ? JSON.stringify(loadOptions.requireGroupCount) : "")
            .set("filter", loadOptions.filter ? JSON.stringify(loadOptions.filter) : "");
        //.set("filter", JSON.stringify(entityType))
        //params.append("entityType", JSON.stringify(entityType));
        //return this.http.get(this.apiUrl + url, { params: params }).toPromise().catch();
        return this.http.get(this.apiUrl + url, { params: params, responseType: 'json' });
    };
    ApiHttpService.prototype.GETTreeData = function (url, loadOptions) {
        //let params: HttpParams = new HttpParams()
        //    .set("sort", JSON.stringify(loadOptions.sort))
        //    .set("filter", JSON.stringify(loadOptions.filter))
        //    .set("group", JSON.stringify(loadOptions.group))
        //    .set("parentIds", JSON.stringify(loadOptions.parentIds))
        //    .set("requireTotalCount", loadOptions.requireTotalCount ? loadOptions.requireTotalCount : "")
        //    .set("primaryKey", loadOptions.primaryKey ? "" : "")
        //    .set("remoteGrouping", loadOptions.remoteGrouping ? loadOptions.remoteGrouping : "")
        //    .set("remoteSelect", loadOptions.remoteSelect ? loadOptions.remoteSelect : "")
        //    .set("preSelect", loadOptions.preSelect ? loadOptions.preSelect : "")
        //    .set("select", loadOptions.select ? loadOptions.select : "")
        //    .set("groupSummary", JSON.stringify(loadOptions.groupSummary))
        //    .set("defaultSort", JSON.stringify(loadOptions.defaultSort))
        //    .set("totalSummary", JSON.stringify(loadOptions.totalSummary))
        //    .set("take", loadOptions.take)
        //    .set("skip", loadOptions.skip)
        //    .set("isCountQuery", JSON.stringify(loadOptions.isCountQuery))
        //    .set("requireGroupCount", JSON.stringify(loadOptions.requireGroupCount))        
        var params = new http_1.HttpParams();
        [
            "sort",
            "filter",
            "group",
            "parentIds",
            "requireTotalCount",
            "primaryKey",
            "remoteGrouping",
            "remoteSelect",
            "preSelect",
            "select",
            "groupSummary",
            "defaultSort",
            "totalSummary",
            "group",
            "sort",
            "take",
            "skip",
            "isCountQuery",
            "requireGroupCount",
            "parentIds",
        ].forEach(function (i) {
            if (i in loadOptions)
                params = params.set(i, JSON.stringify(loadOptions[i]));
        });
        return this.http.get(this.apiUrl + url, { params: params, responseType: 'json' });
    };
    ApiHttpService.prototype.getApiUrl = function (url) {
        return this.apiUrl + url;
    };
    ApiHttpService.prototype.getRootUrl = function (url) {
        return this.rootUrl + url;
    };
    ApiHttpService.prototype.DownloadFile = function (url) {
        return this.http.get(this.apiUrl + url, { responseType: 'blob' });
    };
    ApiHttpService.prototype.GET = function (url) {
        return this.http.get(this.apiUrl + url, { responseType: 'json' });
    };
    ApiHttpService.prototype.POST = function (url, data) {
        return this.http.post(this.apiUrl + url, data, { responseType: 'json' });
    };
    ApiHttpService.prototype.PUT = function (url, data) {
        return this.http.put(this.apiUrl + url, data, { responseType: 'json' });
    };
    ApiHttpService.prototype.DELETE = function (url) {
        return this.http.delete(this.apiUrl + url, { responseType: 'json' });
    };
    ApiHttpService = __decorate([
        core_1.Injectable()
    ], ApiHttpService);
    return ApiHttpService;
}());
exports.ApiHttpService = ApiHttpService;
//# sourceMappingURL=api-http.service.js.map