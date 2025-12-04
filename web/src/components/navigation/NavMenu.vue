<script setup lang="ts">
import Avatar from 'primevue/avatar';
import Button from 'primevue/button';
import Menu from 'primevue/menu';
import Menubar from 'primevue/menubar';
import type { MenuItem } from 'primevue/menuitem';
import { ref } from 'vue';
import { $dt } from '@primeuix/themes';

import IconBoardGame from '../icons/IconBoardGame.vue';
import NavMenuItem from './NavMenuItem.vue';

const items = ref<MenuItem[]>([
  {
    label: 'Home',
    icon: 'pi pi-home',
    url: '/'
  },
  {
    label: 'Games',
    icon: 'pi pi-th-large',
    badge: 4,
    items: [
      {
        label: 'All Games',
        icon: 'pi pi-list',
        url: '/games'
      },
      {
        label: 'Add New Game',
        icon: 'pi pi-plus-circle',
        url: '/games/new'
      }
    ]
  },
  {
    label: 'About',
    icon: 'pi pi-info-circle',
    url: '/about'
  }
]);
const userMenu = ref();
const userMenuItems = ref<MenuItem[]>([
  {
    label: 'User',
    items: [
      {
        label: 'Profile',
        icon: 'pi pi-user',
        url: '/profile'
      },
      {
        label: 'Login',
        icon: 'pi pi-sign-in',
        url: '/login'
      }
    ]
  }
]);

const iconColor = $dt('primary.500').value as string;

const toggleUserMenu = (event: PointerEvent) => {
  userMenu.value.toggle(event);
};
</script>

<template>
  <Menubar :model="items" class="menu" breakpoint="768px">
    <template #start>
      <IconBoardGame class="menu__icon" :iconColor="iconColor" />
    </template>
    <template #item="{ item, props, hasSubmenu, root }">
      <a
        v-if="hasSubmenu"
        role="button"
        v-ripple
        v-bind="props.action"
        class="menu-item"
      >
        <NavMenuItem :item="item" :hasSubmenu="hasSubmenu" :root="root" />
      </a>
      <router-link
        v-else
        v-ripple
        v-bind="props.action"
        class="p-menubar-item-link menu-item"
        :to="item.url ?? '#'"
      >
        <NavMenuItem :item="item" :hasSubmenu="hasSubmenu" :root="root" />
      </router-link>
    </template>
    <template #end>
      <Button
        type="button"
        @click="toggleUserMenu"
        variant="link"
        class="menu-user-button"
        aria-haspopup="true"
        aria-controls="overlay_menu"
      >
        <Avatar label="U" size="normal" shape="circle" />
      </Button>
      <Menu
        ref="userMenu"
        id="overlay_menu"
        :model="userMenuItems"
        :popup="true"
      />
    </template>
  </Menubar>
</template>

<style lang="scss" scoped>
.menu {
  border-radius: 1rem;
}
.menu__icon {
  width: 3rem;
  height: 3rem;
}
.menu-item {
  display: flex;
  align-items: center;
  background: transparent;
  font-size: 1rem;
  border: 0;
  outline: none;
}

.menu-user-button {
  padding: 0;
  margin-left: 2rem;

  @media screen and (min-width: 768px) {
    margin-left: 5rem;
  }
}
</style>
