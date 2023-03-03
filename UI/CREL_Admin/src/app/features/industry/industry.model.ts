export class Industry {
  id: number;
  description: null;
  name: string;
  status: number;

  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      this.description = res.description ? res.description.trim() : "";
      this.name = res.name.trim();
      this.status = res.status;
    } else {
      this.description = null;
      this.name = null;
      this.status = 1;
    }
  }
}
