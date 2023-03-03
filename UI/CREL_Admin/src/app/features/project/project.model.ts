export class Project {
  id: number;
  status: number;
  description: string;
  name: string;
  districtId: number;
  districtName: string;
  handoverYear: Date;
  company: string;

  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      this.description = res.description ? res.description.trim() : "";
      this.name = res.name.trim();
      this.status = res.status;
      this.districtId = res.districtId;
      this.handoverYear = res.handoverYear;
      this.company = res.company.trim();
    } else {
      this.description = null;
      this.name = null;
      this.status = 1;
      this.districtId = null;
    }
  }
}
