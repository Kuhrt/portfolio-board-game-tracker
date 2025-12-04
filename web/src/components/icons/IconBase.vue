<script lang="ts" setup>
import type { IconProps } from '@/models/icons/IconProps';

const props = defineProps<IconProps & { iconName: string }>();
</script>

<template>
  <svg
    xmlns="http://www.w3.org/2000/svg"
    :width="props.width"
    :height="props.height"
    :viewBox="props.viewBox ?? '0 0 18 18'"
    role="presentation"
  >
    <title :id="props.iconName" lang="en">{{ props.iconName }} icon</title>
    <g :fill="props.iconColor">
      <slot />
    </g>
  </svg>
</template>
