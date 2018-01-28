import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "convertToTimeFormat"
})

export class ConvertToTimeFormatPipe implements PipeTransform {
    transform(value: number): string {
        if (value != null){
            return (value > 9 ? '' : '0') + value + (':00');            
        }
    }    
}