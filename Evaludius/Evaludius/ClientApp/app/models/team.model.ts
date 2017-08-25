

export class Team {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: number, name?: string, description?: string, yearOfBirth?: number, manager?: string) {

        this.id = id;
        this.name = name; 
        this.description = description;
        this.yearOfBirth = yearOfBirth;
        this.manager = manager;
       
        
    }


    


    public id: number;
    public name: string;
    public description: string;
    public yearOfBirth: number;
    public manager: string; 
 
    
   
}