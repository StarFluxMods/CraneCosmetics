using Controllers;
using Kitchen;
using MessagePack;
using System;
using CraneCosmetics.Components;
using CraneCosmetics.Utility;
using KitchenData;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraneCosmetics.Views
{
    public class ManageActiveCosmeticsSubview : UpdatableObjectView<ManageActiveCosmeticsSubview.ViewData>, ISpecificViewResponse
    {
        public class UpdateView : ResponsiveViewSystemBase<ViewData, ResponseData>, IModSystem
        {
            EntityQuery Query;

            protected override void Initialise()
            {
                base.Initialise();

                Query = GetEntityQuery(typeof(CLinkedView), typeof(CIsCraneMode), typeof(CCraneCosmetics), typeof(CPlayer));
            }

            protected override void OnUpdate()
            {
                using (NativeArray<Entity> entities = Query.ToEntityArray(Allocator.Temp))
                {
                    foreach (Entity entity in entities)
                    {
                        if (!Require(entity, out CLinkedView view)) continue;
                        if (!Require(entity, out CCraneCosmetics cCraneCosmetics)) continue;
                        if (!Require(entity, out CPlayer cPlayer)) continue;

                        SendUpdate(view.Identifier, new ViewData
                        {
                            Source = cPlayer.InputSource,
                            PlayerID = cPlayer.ID
                        }, MessageType.SpecificViewUpdate);


                        ApplyUpdates(view.Identifier, (data) =>
                        {
                            cCraneCosmetics.CraneCosmetics = data.Cosmetics;
                            Set(entity, cCraneCosmetics);
                        }, only_final_update: true);
                    }
                }
            }
        }

        [MessagePackObject()]
        public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int Source;
            [Key(1)] public int PlayerID;

            public IUpdatableObject GetRelevantSubview(IObjectView view)
            {
                return view.GetSubView<ManageActiveCosmeticsSubview>();
            }

            public bool IsChangedFrom(ViewData check)
            {
                return true;
            }
        }

        [MessagePackObject]
        public struct ResponseData : IResponseData, IViewResponseData
        {
            [Key(0)] public DataObjectList Cosmetics;
        }

        protected override void UpdateData(ViewData data)
        {
            if (data.Source == InputSourceIdentifier.Identifier)
            {
                if (Session.PlayerNames.TryGetValue(data.PlayerID, out string name))
                {
                    DataObjectList cosmetics = SavedCosmeticManager.GetCosmetics(data.PlayerID);
                    if (Callback != null)
                    {
                        Callback.Invoke(new ResponseData
                        {
                            Cosmetics = cosmetics
                        }, typeof(ResponseData));
                    }
                }
            }
        }

        private Action<IResponseData, Type> Callback;

        public void SetCallback(Action<IResponseData, Type> callback)
        {
            Callback = callback;
        }
    }
}