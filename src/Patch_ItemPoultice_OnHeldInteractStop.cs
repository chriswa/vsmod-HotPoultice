using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace HotPoultice {
  [HarmonyPatch(typeof(ItemPoultice))]
  [HarmonyPatch("OnHeldInteractStop")]
  public class Patch_ItemPoultice_OnHeldInteractStop {
    static bool Prefix(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel) {
      if (secondsUsed > 0.7f && byEntity.World.Side == EnumAppSide.Server) {
        float health = slot.Itemstack.Collectible.Attributes["health"].AsFloat(0f);
        var isBuffActive = BuffStuff.BuffManager.IsBuffActive(byEntity, "HealOverTime");
        // only consume item and apply buff if there isn't a buff already active
        if (!isBuffActive) {
          var buff = new HealOverTimeBuff();
          buff.init(health);
          buff.Apply(byEntity);
          slot.TakeOut(1);
          slot.MarkDirty();
        }
      }
      return false; // skip original method
    }
  }
}
