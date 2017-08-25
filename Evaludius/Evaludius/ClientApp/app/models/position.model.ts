

export class PlayerPosition {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: number, name?: string, isFieldPosition?: boolean) {

        this.id = id;
        this.name = name; 
        this.isFieldPosition = isFieldPosition;
        
       
        
    }


    


    public id: number;
    public name: string;
    public isFieldPosition: boolean;
   
 
    
   
}