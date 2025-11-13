"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var EmployeeService = /** @class */ (function () {
    function EmployeeService(apiHttpService) {
        this.apiHttpService = apiHttpService;
        this.url = '';
    }
    EmployeeService.prototype.getEmployeeInitData = function (id) {
        this.url = 'hrm/employee/get-employee-initial-data/' + id;
        return this.apiHttpService.GET(this.url);
    };
    EmployeeService.prototype.getEmployeePageTabs = function (id) {
        this.url = 'hrm/employee/get-employee-page-tabs/' + id;
        return this.apiHttpService.GET(this.url);
    };
    EmployeeService.prototype.getEmployeeDetails = function (id) {
        this.url = 'hrm/employee/get-employee-by-id/' + id;
        return this.apiHttpService.GET(this.url);
    };
    EmployeeService.prototype.createEmployee = function (fileName, data) {
        this.url = 'hrm/employee/create-employee?imageFileName=' + fileName;
        return this.apiHttpService.POST(this.url, data);
    };
    EmployeeService.prototype.updateEmployee = function (fileName, id, data) {
        this.url = 'hrm/employee/update-employee/' + id + '?imageFileName=' + fileName;
        return this.apiHttpService.PUT(this.url, data);
    };
    EmployeeService = __decorate([
        core_1.Injectable()
    ], EmployeeService);
    return EmployeeService;
}());
exports.EmployeeService = EmployeeService;
//# sourceMappingURL=employee.service.js.map