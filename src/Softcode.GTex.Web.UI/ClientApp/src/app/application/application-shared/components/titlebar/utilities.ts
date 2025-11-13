
/**
 * This enum is used to identify which type of toolbar will be populated for a page
 **/
export enum ToolbarType {
    ListPage = 1,
    ListTabPage = 2,
    DetailPage = 3,
    DetailTabPage = 4,
    LinkPage = 5,
    TabPage = 6,

}

export enum DetailPageAction {
    Save = 1,
    SaveAndNew = 2,
    SaveAndClose = 3,
    Close = 4
}
 
export enum UserType {
    Contact = 10801,
    Employee = 10802,
    User = 10803
}

/**
 * Write all the possible pattern for the system we need [regular expression]
 * */
export class PatterMatch {
    public static EmailPattern = "\\w+([-+.']\\w+)*.?@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";    
}
