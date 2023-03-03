import { District } from "./district.model";
import { Location } from "./location.model";

export class Ward {
  id: number;
  districtId: number;
  name: string;
  status: number;
  teamId: number;
  district: District;
  districtName: string;
  locations: Location[];

  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      this.teamId = res.teamId;
      this.name = res.name;
      this.status = res.status;
      this.districtId = res.districtId;
    } else {
      this.teamId = null;
      this.name = null;
      this.status = 1;
      this.districtId = null;
    }
  }
}
