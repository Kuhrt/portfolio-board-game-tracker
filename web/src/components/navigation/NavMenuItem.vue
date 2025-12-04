<script lang="ts" setup>
import type { MenuItem } from 'primevue/menuitem';

const props = defineProps<{
  item: MenuItem;
  hasSubmenu: boolean;
  root: boolean;
}>();
</script>

<template>
  <span>{{ props.item.label }}</span>
  <i
    v-if="props.hasSubmenu"
    :class="[
      'pi pi-angle-down ml-auto icon',
      { 'pi-angle-down': props.root, 'pi-angle-right': !props.root }
    ]"
  ></i>
</template>
