"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var EmployeeDetailComponent = /** @class */ (function () {
    function EmployeeDetailComponent(route, router, employeeService) {
        var _this = this;
        this.route = route;
        this.router = router;
        this.employeeService = employeeService;
        this.tabs = [];
        //entityModel: EntityModel = new EntityModel();
        this.employeeId = 0;
        this.tabPageServiceStyle = 'tab-item-invisible';
        this.entityType = 102;
        this.route.params.subscribe(function (params) {
            if (params['employeeId'] != undefined) {
                _this.employeeId = params['employeeId'];
            }
        });
        //this.entityModel.entityId = this.contactId;
        //this.entityModel.entityType = 102;
        this.initTabs();
    }
    /**
     * initialize tab
     * */
    EmployeeDetailComponent.prototype.initTabs = function () {
        var _this = this;
        if (this.employeeId > 0) {
            this.employeeService.getEmployeePageTabs(this.employeeId).subscribe(function (data) {
                _this.tabs = data.result.tabItems;
            });
        }
    };
    /**
     * Event
     * */
    EmployeeDetailComponent.prototype.ngAfterViewInit = function () {
        //if it is new mode then hide other tab except primary component
        if (this.employeeId != null && this.employeeId != undefined) {
            this.tabPageServiceStyle = "tab-item-visible";
        }
    };
    EmployeeDetailComponent = __decorate([
        core_1.Component({
            selector: '.app-employee-detail',
            templateUrl: './employee-detail.component.html',
            styleUrls: ['./employee-detail.component.scss'],
        })
    ], EmployeeDetailComponent);
    return EmployeeDetailComponent;
}());
exports.EmployeeDetailComponent = EmployeeDetailComponent;
//# sourceMappingURL=employee-detail.component.js.map