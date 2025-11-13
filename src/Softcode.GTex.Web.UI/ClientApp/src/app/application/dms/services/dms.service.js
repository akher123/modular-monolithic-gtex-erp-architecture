"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var file_saver_1 = require("file-saver");
var DmsService = /** @class */ (function () {
    function DmsService(clipboardService, apiHttpService) {
        this.clipboardService = clipboardService;
        this.apiHttpService = apiHttpService;
        this.url = '';
    }
    DmsService.prototype.getDocument = function (id) {
        this.url = 'dms/document/get-document/' + id;
        return this.apiHttpService.GET(this.url);
    };
    DmsService.prototype.getDocumentsByEntity = function (entityTypeId, entityId) {
        this.url = 'dms/document/get-documents-by-entity/' + entityTypeId + '/' + entityId;
        return this.apiHttpService.GET(this.url);
    };
    DmsService.prototype.saveDocumentsByEntity = function (entityTypeId, entityId, fileDataSource) {
        this.url = 'dms/document/save-documents-by-entity/' + entityTypeId + '/' + entityId;
        return this.apiHttpService.PUT(this.url, fileDataSource);
    };
    DmsService.prototype.saveDocuments = function (data) {
        this.url = 'dms/document/save-documents';
        return this.apiHttpService.POST(this.url, data);
    };
    DmsService.prototype.updateDocument = function (id, data) {
        this.url = 'dms/document/update-document/' + id;
        return this.apiHttpService.PUT(this.url, data);
    };
    DmsService.prototype.downloadLatestFile = function (id) {
        var _this = this;
        this.url = 'dms/document/get-download-file-name-by-id/' + id;
        return this.apiHttpService.GET(this.url).subscribe(function (res) {
            _this.url = 'dms/document/download-lastest-file/' + id;
            _this.apiHttpService.DownloadFile(_this.url).subscribe(function (data) {
                var blob = new Blob([data], { type: data.type });
                file_saver_1.default(blob, res.result);
            });
        });
    };
    DmsService.prototype.downloadProtectedFile = function (key) {
        var _this = this;
        this.url = 'dms/document/get-protected-file-name/' + key;
        return this.apiHttpService.GET(this.url).subscribe(function (res) {
            _this.url = 'dms/document/download-protected/' + key;
            _this.apiHttpService.DownloadFile(_this.url).subscribe(function (data) {
                var blob = new Blob([data], { type: data.type });
                file_saver_1.default(blob, res.result);
            });
        });
    };
    DmsService.prototype.downloadFile = function (id, fileName) {
        this.url = 'dms/document/download-file-by-id/' + id;
        this.apiHttpService.DownloadFile(this.url).subscribe(function (data) {
            var blob = new Blob([data], { type: data.type });
            file_saver_1.default(blob, fileName);
        });
    };
    DmsService.prototype.getDxGridRecords = function (url, params) {
        return this.apiHttpService.GETDXGrid(url, params);
    };
    DmsService.prototype.deleteRecord = function (url, id) {
        return this.apiHttpService.DELETE(url + id);
    };
    DmsService.prototype.deleteRecords = function (url, ids) {
        return this.apiHttpService.POST(url, ids);
    };
    DmsService = __decorate([
        core_1.Injectable()
    ], DmsService);
    return DmsService;
}());
exports.DmsService = DmsService;
//# sourceMappingURL=dms.service.js.map