"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var devextreme_angular_1 = require("devextreme-angular");
var custom_store_1 = require("devextreme/data/custom_store");
var TreeViewDropDownBoxComponent = /** @class */ (function () {
    /**
     *
     * @param messageService
     */
    function TreeViewDropDownBoxComponent(messageService, utilityService, apiHttpService) {
        this.messageService = messageService;
        this.utilityService = utilityService;
        this.apiHttpService = apiHttpService;
        this.disabled = false;
        this._isRequired = false;
        this.labelFor = "";
        this.isDropDownBoxOpened = false;
        this.treeBoxValue = "";
        this.onSelectedValueChange = new core_1.EventEmitter();
    }
    Object.defineProperty(TreeViewDropDownBoxComponent.prototype, "isRequired", {
        set: function (value) {
            if (value) {
                this._isRequired = value;
                this.attachValidationToControl();
            }
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(TreeViewDropDownBoxComponent.prototype, "seletedValue", {
        get: function () {
            return this.treeBoxValue;
        },
        set: function (value) {
            this.treeBoxValue = value;
        },
        enumerable: true,
        configurable: true
    });
    TreeViewDropDownBoxComponent.prototype.ngOnInit = function () {
        this.attachValidationToControl();
    };
    TreeViewDropDownBoxComponent.prototype.attachValidationToControl = function () {
        this.treeViewDropDownValidation.validationRules = [{ type: 'custom', validationCallback: this.validationCallback, message: '' }];
        if (this._isRequired) {
            this.treeViewDropDownValidation.validationRules = [{ type: 'required', message: this.labelFor + ' is required.' }];
        }
    };
    TreeViewDropDownBoxComponent.prototype.validationCallback = function () {
        return true;
    };
    TreeViewDropDownBoxComponent.prototype.syncTreeViewSelection = function (e) {
        if (!this.treeView)
            return;
        if (!this.treeBoxValue) {
            this.treeView.instance.unselectAll();
        }
        else {
            this.treeView.instance.selectItem(this.treeBoxValue);
        }
    };
    TreeViewDropDownBoxComponent.prototype.getSelectedItemsKeys = function (items) {
        var result = [], that = this;
        items.forEach(function (item) {
            if (item.selected) {
                result.push(item.key);
            }
            if (item.items.length) {
                result = result.concat(that.getSelectedItemsKeys(item.items));
            }
        });
        return result;
    };
    TreeViewDropDownBoxComponent.prototype.treeView_itemSelectionChanged = function (e) {
        this.isDropDownBoxOpened = false;
        var nodes = e.component.getNodes();
        this.treeBoxValue = this.getSelectedItemsKeys(nodes).join("");
        this.onSelectedValueChange.emit(this.treeBoxValue);
    };
    TreeViewDropDownBoxComponent.prototype.makeAsyncDataSource = function (utilityService, url) {
        return new custom_store_1.default({
            loadMode: "raw",
            key: "sId",
            load: function () {
                return utilityService.getDataSource(url).toPromise().then(function (response) {
                    return response.result;
                });
            }
        });
    };
    TreeViewDropDownBoxComponent.prototype.loadDatasource = function (url) {
        this.treeDataSource = this.makeAsyncDataSource(this.utilityService, url);
    };
    __decorate([
        core_1.Input('disabled')
    ], TreeViewDropDownBoxComponent.prototype, "disabled", void 0);
    __decorate([
        core_1.Input('IsRequired') //isRequired = false;
    ], TreeViewDropDownBoxComponent.prototype, "isRequired", null);
    __decorate([
        core_1.Input('LabelFor')
    ], TreeViewDropDownBoxComponent.prototype, "labelFor", void 0);
    __decorate([
        core_1.Input('SelectedValue')
    ], TreeViewDropDownBoxComponent.prototype, "seletedValue", null);
    __decorate([
        core_1.Output()
    ], TreeViewDropDownBoxComponent.prototype, "onSelectedValueChange", void 0);
    __decorate([
        core_1.ViewChild(devextreme_angular_1.DxTreeViewComponent)
    ], TreeViewDropDownBoxComponent.prototype, "treeView", void 0);
    __decorate([
        core_1.ViewChild('treeViewDropDownValidation')
    ], TreeViewDropDownBoxComponent.prototype, "treeViewDropDownValidation", void 0);
    TreeViewDropDownBoxComponent = __decorate([
        core_1.Component({
            selector: 'app-tree-view-drop-down-box',
            templateUrl: './app-tree-view-drop-down-box.component.html',
            styleUrls: ['./app-tree-view-drop-down-box.component.scss'],
        })
    ], TreeViewDropDownBoxComponent);
    return TreeViewDropDownBoxComponent;
}());
exports.TreeViewDropDownBoxComponent = TreeViewDropDownBoxComponent;
//# sourceMappingURL=app-tree-view-drop-down-box.component.js.map