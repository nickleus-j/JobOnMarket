import { CurrencyDto } from "../Currency/CurrencyDto";
export interface JobDetailDto {
    id:number;
    startDate:Date;
    dueDate:Date;
    budget:number;
    currency:CurrencyDto;
    description:string;
}