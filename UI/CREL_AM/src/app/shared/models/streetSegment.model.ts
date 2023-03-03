export class StreetSegment {
  id: number;
  description: string;
  name: string;
  status: number;
  districtId: number;

  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      this.description = res.description;
      this.name = res.name;
      this.status = res.status;
      this.districtId = res.districtId;
    } else {
      this.description = null;
      this.name = null;
      this.status = 1;
      this.districtId = null;
    }
  }
}
