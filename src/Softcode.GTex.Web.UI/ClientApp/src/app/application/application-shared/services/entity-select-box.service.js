"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var EntitySelectBoxService = /** @class */ (function () {
    function EntitySelectBoxService(apiHttpService) {
        this.apiHttpService = apiHttpService;
    }
    EntitySelectBoxService.prototype.getEntitiesByBusinessProfileId = function (entityTypeId, businessProfileId) {
        var url = 'system-service/application-entity/get-entities/' + entityTypeId + '/' + businessProfileId;
        
        return this.apiHttpService.GET(url);
    };
    EntitySelectBoxService.prototype.getCompaniesByBusinessProfileId = function (businessProfileId) {
        var url = 'system-service/application-entity/get-companies/' + businessProfileId;
        
        return this.apiHttpService.GET(url);
    };
    EntitySelectBoxService.prototype.getContactsByBusinessProfileId = function (businessProfileId) {
        var url = 'system-service/application-entity/get-contacts/' + businessProfileId;
        
        return this.apiHttpService.GET(url);
    };
    EntitySelectBoxService = __decorate([
        core_1.Injectable()
    ], EntitySelectBoxService);
    return EntitySelectBoxService;
}());
exports.EntitySelectBoxService = EntitySelectBoxService;
//# sourceMappingURL=entity-select-box.service.js.map
