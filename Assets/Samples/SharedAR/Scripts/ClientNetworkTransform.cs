// Copyright 2022-2024 Niantic.

using Unity.Netcode.Components;

// TODO: Wrap with Niantic.Lightship.AR.Samples namespace
public class ClientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
