export default class HelperUtils {
    public static getDateString(date: Date): string {
        return `${date.getDate()}/${date.getMonth()}/${date.getFullYear()}`;
    }
}