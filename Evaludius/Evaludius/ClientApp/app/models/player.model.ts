// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

export class Player {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: number, firstName?: string, lastName?: string, yearOfBirth?: number, position?: string, isActive?: boolean, teams?: string[]) {

        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.yearOfBirth = yearOfBirth;
        this.position = position;
        this.isActive = isActive;
        this.teams = teams;
        
    }


    


    public id: number;
    public firstName: string;
    public lastName: string;
    public yearOfBirth: number;
    public position: string; 
    public teams: string[];
    public isActive: boolean;
    public fullName = this.firstName + " " + this.lastName;
   
}