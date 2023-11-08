class LoginCommand {
    public UsernameOrEmail?: string;
    public Password?: string;
    public UserType?: string;
    constructor(usernameOrEmail?: string, password?: string, userType?: string ) {
        this.UsernameOrEmail = usernameOrEmail;
        this.Password = password;
        this.UserType = userType;
    }
}

class LoginResponse {
    public AccessToken?: string;
    public RefreshToken?: string;
}

export {LoginCommand, LoginResponse}