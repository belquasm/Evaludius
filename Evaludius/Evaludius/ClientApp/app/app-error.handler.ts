// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

import { Injectable, ErrorHandler } from "@angular/core";
import { AlertService, MessageSeverity } from './services/alert.service';


@Injectable()
export class AppErrorHandler extends ErrorHandler {

    //private alertService: AlertService;

    constructor() {
        super(true);
    }


    handleError(error) {
   
        if (confirm("Fatal Error!\nAn unresolved error has occured. Do you want to reload the page to correct this?\n\nError: " + error.message))
            window.location.reload(true);

        super.handleError(error);
    }
}