namespace KontursvetStore.Core.Constants;

public static class ErrorMessages
{
    public static readonly string NAME_NULL_OR_LONG = $"Имя не может быть пустым или больше чем {StoreAppConstants.MAX_NAME_LENGTH} символов";
    public static readonly string EMAIL_NULL_OR_LONG = $"Поле email не может быть пустым или больше чем {StoreAppConstants.MAX_NAME_LENGTH} символов";
}