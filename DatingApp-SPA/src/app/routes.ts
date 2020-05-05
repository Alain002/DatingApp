import { AuthGuard } from './_guards/auth.guard';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberListComponent } from './member-list/member-list.component';
import { HomeComponent } from './home/home.component';
import { Component } from '@angular/core';
import { Routes } from '@angular/router';

export const appRoutes: Routes =  [
  { path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MemberListComponent},
      { path: 'messages', component: MessagesComponent},
      { path: 'list', component: ListsComponent},
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'}
];
