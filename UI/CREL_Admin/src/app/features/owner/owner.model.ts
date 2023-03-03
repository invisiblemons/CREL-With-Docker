export class Owner {
    id:          number;
    name:        string;
    phoneNumber: string;
    email:       string;
    gender:      boolean;
    status:      number;

    constructor(res, isCreate) {
        if (res) {
          if (!isCreate) this.id = res.id;
          this.name = res.name.trim();
          this.phoneNumber = res.phoneNumber;
          this.email = res.email.trim();
          this.gender = res.gender;
          this.status = res.status;
        } else {
          this.name = null;
          this.phoneNumber = null;
          this.email = null;
          this.gender = null;
          this.status = 1;
        }
      }
}
