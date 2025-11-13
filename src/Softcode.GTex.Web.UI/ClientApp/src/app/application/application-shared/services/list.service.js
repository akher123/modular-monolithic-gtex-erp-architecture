"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ListService = /** @class */ (function () {
    function ListService(apiHttpService) {
        this.apiHttpService = apiHttpService;
        this.url = '';
    }
    ListService.prototype.getPageInfoByRoutingUrl = function (routingUrl) {
        this.url = 'application-service/application-page/get-application-list-page-by-routing-url?routingUrl=' + routingUrl;
        return this.apiHttpService.GET(this.url);
    };
    ListService.prototype.getPageInfoByName = function (name) {
        this.url = 'application-service/application-page/get-application-list-page-by-name?name=' + name;
        return this.apiHttpService.GET(this.url);
    };
    ListService.prototype.getDxGridRecords = function (url, params) {
        return this.apiHttpService.GETDXGrid(url, params);
    };
    ListService.prototype.deleteRecord = function (url, id) {
        return this.apiHttpService.DELETE(url + id);
    };
    ListService.prototype.deleteRecords = function (url, ids) {
        return this.apiHttpService.POST(url, ids);
    };
    ListService = __decorate([
        core_1.Injectable()
    ], ListService);
    return ListService;
}());
exports.ListService = ListService;
//# sourceMappingURL=list.service.js.map