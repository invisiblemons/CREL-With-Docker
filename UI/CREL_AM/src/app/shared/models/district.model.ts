export class District {
  id: number;
  name: string;
  status: number;
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
