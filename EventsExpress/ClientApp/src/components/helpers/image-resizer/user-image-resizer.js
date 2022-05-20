import React from "react";
import ImageResizer from "./image-resizer";

export default function UserImageResizer(props) {
  const computeResizedPhoto = (image, pixelCrop, maxZoom, zoom) => {
    const canvas = document.createElement("canvas");
    const ctx = canvas.getContext("2d");

    let newCropSize = {
      width: Math.round((pixelCrop.width / maxZoom) * zoom),
      height: Math.round((pixelCrop.height / maxZoom) * zoom),
      x: 0,
      y: 0,
    };

    canvas.width = newCropSize.width;
    canvas.height = newCropSize.height;

    ctx.drawImage(
      image,
      pixelCrop.x,
      pixelCrop.y,
      pixelCrop.width,
      pixelCrop.height,
      newCropSize.x,
      newCropSize.y,
      newCropSize.width,
      newCropSize.height
    );
    return canvas.toDataURL("image/jpeg");
  };

  const getCompressionRatio = (image, containerName) => {
    const container = document.querySelector(containerName); //".crop-container"
    const styles = getComputedStyle(container);
    const height = parseInt(styles.height);
    const width = parseInt(styles.width);
    return image.height >= image.width
      ? image.height / height
      : image.width / width;
  };

  return (
    <ImageResizer
      {...props}
      cropSize={{ width: 110, height: 110 }}
      computeResizedPhoto={computeResizedPhoto}
      getCompressionRatio={getCompressionRatio}
    />
  );
}
