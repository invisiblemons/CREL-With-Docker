import { Ward } from "./ward.model";

export class District {
  id: number;
  name: string;
  status: number;
  wards: Ward[];
  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      this.name = res.name;
      this.status = res.status;
    } else {
      this.name = null;
      this.status = 1;
    }
  }
}
