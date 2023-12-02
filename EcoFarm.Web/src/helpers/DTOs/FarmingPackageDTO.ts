export default class FarmingPackageDTO {
    public id?: string;
    public code?: string;
    public name?: string;
    public sellerEnterpriseId?: string;
    public sellerEnterpriseCode?: string;
    public sellerEnterpriseName?: string;
    public description?: string;
    public estimatedStartTime?: Date;
    public estimatedEndTime?: Date;
    public closeRegisterTime?: Date;
    public startTime?: Date;
    public endTime?: Date;
    public price?: number;
    public currency?: number;
    public currencyName?: string;
    public quantityStart?: number;
    quantityRegistered?: number;
    quantityRemain?: number;
    rejectReason?: string;
    servicePackageApprovalStatus?: number;
    servicePackageApprovalStatusName?: string;
    packageType?: number;
    packageTypeName?: string;
    serviceTypeName?: string;
    registeredUsers?: RegisteredUser[] = [];
}

export interface RegisteredUser {
    accountId: string;
    username: string;
    name: string;
    registeredTime: Date;
}