import { Team } from "../team/team.model";

export class AreaManager {
  id: number;
  userName: string;
  birthday: Date;
  password: string;
  gender: boolean;
  name: string;
  status: number;
  avatarFileId: string;
  avatarLink: string;
  phoneNumber: string;
  email: string;
  teamName: string;
  isHasTeam: boolean;

  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      this.userName = res.userName.toLowerCase().trim();
      this.birthday = res.birthday;
      this.password = res.password;
      this.gender = res.gender;
      this.name = res.name.trim();
      this.status = res.status;
      this.avatarFileId = res.avatarFileId;
      this.avatarLink = res.avatarLink;
      this.phoneNumber = res.phoneNumber;
      this.email = res.email.trim();
    } else {
      this.userName = null;
      this.birthday = null;
      this.gender = null;
      this.name = null;
      this.status = 2;
      this.avatarFileId = null;
      this.avatarLink = null;
      this.phoneNumber = null;
      this.email = null;
    }
  }
}
