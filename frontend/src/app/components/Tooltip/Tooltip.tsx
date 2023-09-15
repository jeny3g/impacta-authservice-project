import React from "react";

import styles from "./tooltip.module.css";

type ToastContainerProps = {
  children?: React.ReactNode;
  message?: string;
};

export default function Tooltip({ message, children }: ToastContainerProps) {
  if (!message) return null;

  return (
    <div className={styles.container}>
      {children}
      <span className={styles.span}>{message}</span>
    </div>
  );
}
