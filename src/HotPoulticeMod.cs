using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using System.Reflection;

[assembly: ModInfo("HotPoultice")]

namespace HotPoultice {
  public class HotPoulticeMod : ModSystem {
    static private bool alreadyPatched = false;

    public override void StartServerSide(ICoreServerAPI api) {
      BuffStuff.BuffManager.Initialize(api, this);
      BuffStuff.BuffManager.RegisterBuffType("HealOverTime", typeof(HealOverTimeBuff));

      // api.RegisterCommand("hot", "heal over time (2.5 hp)", "/hot", (IServerPlayer player, int groupId, CmdArgs args) => {
      //   var buff = new HealOverTimeBuff();
      //   buff.init(2.5);
      //   buff.Apply(player.Entity);
      // }, Privilege.chat);

      // api.RegisterCommand("hurt", "instant damage (4.5 hp)", "/hurt", (IServerPlayer player, int groupId, CmdArgs args) => {
      //   player.Entity.ReceiveDamage(new DamageSource {
      //     Source = EnumDamageSource.Internal,
      //     Type = EnumDamageType.Poison
      //   }, 4.5f);
      // }, Privilege.chat);

      if (!alreadyPatched) {
        var harmony = new Harmony("goxmeor.HotPoultice");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        alreadyPatched = true;
      }
    }
  }
}
