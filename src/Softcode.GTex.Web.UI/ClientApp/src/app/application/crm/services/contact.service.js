"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ContactService = /** @class */ (function () {
    function ContactService(apiHttpService) {
        this.apiHttpService = apiHttpService;
        this.url = '';
    }
    ContactService.prototype.getDxGridRecords = function (id, params) {
        this.url = 'crm/contact/get-contact-company-list/' + id;
        return this.apiHttpService.GETDXGrid(this.url, params);
    };
    /**
     * get contact page details
     * @param id
     */
    ContactService.prototype.getContactPageDetails = function (id) {
        this.url = 'crm/contact/get-contact-details-tab?id=' + id;
        return this.apiHttpService.GET(this.url);
    };
    ContactService.prototype.getContact = function (id) {
        this.url = 'crm/contact/get-contact?id=' + id;
        return this.apiHttpService.GET(this.url);
    };
    //getCompany(id: any) {
    //    this.url = 'crm/contact/get-contact-company-list/' + id;
    //    return this.apiHttpService.GET(this.url);
    //}
    ContactService.prototype.getCompanyByBusinessProfile = function (ids) {
        this.url = 'crm/contact/get-contact-company-list';
        return this.apiHttpService.POST(this.url, ids);
    };
    ContactService.prototype.createContact = function (fileName, data) {
        this.url = 'crm/contact/create-contact?imageFileName=' + fileName;
        return this.apiHttpService.POST(this.url, data);
    };
    ContactService.prototype.updateContact = function (fileName, data) {
        this.url = 'crm/contact/update-contact?imageFileName=' + fileName;
        return this.apiHttpService.PUT(this.url, data);
    };
    ContactService = __decorate([
        core_1.Injectable()
    ], ContactService);
    return ContactService;
}());
exports.ContactService = ContactService;
//# sourceMappingURL=contact.service.js.map