class SignUpCommand {
    public USERNAME?: string;
    public EMAIL?: string;
    public Password?: string;
    public DATE_OF_BIRTH?: Date;
    public Fullname?: string;

    public DESCRIPTION?: string;
    public TAX_CODE?: string;
    public HOTLINE?: string;
    public ADDRESS?: string;
}

class SignUpResponse {

    public Id?: string;
    public Code?: string;
    public Username?: string;
    public Email?: string;
}

export { SignUpCommand, SignUpResponse }