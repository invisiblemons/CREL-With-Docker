import { Industry } from "../industry/industry.model";

export class Brand {
  id: number;
  userName: string;
  password: string;
  totalProperty: number;
  name: string;
  firebaseUid: string;
  email: string;
  phoneNumber: string;
  status: number;
  description: string;
  avatarFileId: string;
  avatarLink: string;
  industryId: number;
  industry: Industry;
  industryName: string;
  brokerId: number;
  registrationNumber: string;
  rejectMessage: string;

  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      if (res.userName) {
        this.userName = res.userName.toLowerCase().trim();
      }
      this.password = res.password;
      this.name = res.name.trim();
      this.firebaseUid = res.firebaseUid;
      this.email = res.email.trim();
      this.phoneNumber = res.phoneNumber;
      this.status = res.status;
      this.description = res.description ? res.description.trim() : "";
      this.avatarFileId = res.avatarFileId;
      this.avatarLink = res.avatarLink;
      this.industryId = res.industryId;
      this.brokerId = res.brokerId;
      this.registrationNumber = res.registrationNumber;
      if (!isCreate) {
        if (res.status === 0) {
          this.rejectMessage = res.rejectMessage.trim();
        } else this.rejectMessage = "";
      }
    } else {
      this.userName = null;
      this.name = null;
      this.firebaseUid = null;
      this.email = null;
      this.phoneNumber = null;
      this.status = 2;
      this.description = null;
      this.avatarFileId = null;
      this.avatarLink = null;
      this.industryId = null;
      this.brokerId = null;
      this.registrationNumber = null;
    }
  }
}
