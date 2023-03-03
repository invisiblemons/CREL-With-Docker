import {Brand} from '../brand/brand.model';
import { Property } from '../property/property.model';
export class BrandRequest {
  id:number;
  brandId: number;
  area: string;
  amount: number;
  amountFrontage: number;
  minPrice: number;
  maxPrice: number;
  minRentalTime: Date;
  maxRentalTime: Date;
  minFloorArea: number;
  maxFloorArea: number;
  description: string;
  brand: Brand;
  properties: Property[];
  status: number;
}
