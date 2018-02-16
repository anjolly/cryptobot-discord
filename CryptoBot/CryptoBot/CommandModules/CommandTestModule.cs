using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot.CommandModules
{
    public class CommandTestModule : ModuleBase<SocketCommandContext>
    {
        [Command("role")]
        [Summary("Test adding to role.")]
        public async Task AddRole([Summary("The mention of a user")] string userMention)
        {
            ulong userId = (ulong)Context.Message.MentionedUsers.FirstOrDefault()?.Id; // get user id via mention

            if (userId == 0)
                await Context.Channel.SendMessageAsync("Error, please mention a user.");
            else
            {
                // Get role
                var role = Context.Guild.Roles.Where(r => r.Name == "Test Role").FirstOrDefault();

                var user = Context.Guild.Users.Where(u => u.Id == userId).FirstOrDefault();

                if (user != null)
                    await user.AddRoleAsync(role);
            }
        }
    }
}
