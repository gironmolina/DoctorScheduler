import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "convertToTimeFormat"
})

export class ConvertToTimeFormatPipe implements PipeTransform {
    transform(value: string): string {
        if (value != null){
            return value.substr(0, value.length - 3);            
        }
    }    
}