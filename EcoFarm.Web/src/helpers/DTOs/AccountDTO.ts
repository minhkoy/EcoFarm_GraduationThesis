export default class AccountDTO {
  AccountId?: string;
  FullName?: string;
  Username?: string;
  Email?: string;
  IsEmailConfirmed?: boolean | null;
  AccountType?: string;
  IsActive?: boolean | null;
  LockedReason?: string;
  AvatarUrl?: string;
}