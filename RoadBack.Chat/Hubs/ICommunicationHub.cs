namespace RoadBack.Chat.SignalRHubs
{
    public interface ICommunicationHub
    {
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="messageg"></param>
        /// <returns></returns>
        Task SendMessageAsync(string userName, string messageg);

        /// <summary>
        /// Update user list
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        Task UpdateUsersAsync(IEnumerable<string> users);
    }
}
